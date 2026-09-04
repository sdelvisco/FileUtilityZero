using FileUtilityZero.Core;

namespace FileUtilityZero.Core.Tests.TestDoubles;

// Hand-written in-memory fake for IFileSystem, letting FileScanner be tested
// against a built-up fake directory tree instead of the real disk.
public sealed class FakeFileSystem : IFileSystem
{
    private readonly HashSet<string> _directories = new();
    private readonly Dictionary<string, List<string>> _filesByDirectory = new();
    private readonly Dictionary<string, List<string>> _subdirectoriesByDirectory = new();
    private readonly Dictionary<string, FileMetadata> _metadataByPath = new();
    private readonly Dictionary<string, byte[]> _contentByPath = new();
    private readonly Dictionary<string, Exception> _enumerateFilesExceptions = new();
    private readonly List<string> _createdDirectories = new();

    public IReadOnlyList<string> CreatedDirectories => _createdDirectories;

    public void AddDirectory(string path)
    {
        if (_directories.Add(path))
        {
            _filesByDirectory[path] = new List<string>();
            _subdirectoriesByDirectory[path] = new List<string>();
        }
    }

    public void AddSubdirectory(string parentPath, string childPath)
    {
        AddDirectory(parentPath);
        AddDirectory(childPath);
        _subdirectoriesByDirectory[parentPath].Add(childPath);
    }

    public string AddFile(
        string directoryPath,
        string fileName,
        long length = 0,
        DateTime? creationTime = null,
        DateTime? lastWriteTime = null,
        DateTime? lastAccessTime = null,
        FileAttributes attributes = default,
        byte[]? content = null)
    {
        AddDirectory(directoryPath);
        string fullPath = directoryPath.TrimEnd('/', '\\') + "/" + fileName;
        _filesByDirectory[directoryPath].Add(fullPath);

        DateTime time = creationTime ?? DateTime.UtcNow;
        _metadataByPath[fullPath] = new FileMetadata(
            fileName,
            fullPath,
            length,
            creationTime ?? time,
            lastWriteTime ?? time,
            lastAccessTime ?? time,
            attributes);
        _contentByPath[fullPath] = content ?? Array.Empty<byte>();

        return fullPath;
    }

    // Makes EnumerateFiles throw for a specific directory, so tests can verify
    // FileScanner logs the error and keeps scanning the rest of the tree.
    public void ThrowWhenEnumeratingFiles(string directoryPath, Exception exception) =>
        _enumerateFilesExceptions[directoryPath] = exception;

    public bool DirectoryExists(string path) => _directories.Contains(path);

    public void CreateDirectory(string path)
    {
        _createdDirectories.Add(path);
        AddDirectory(path);
    }

    public IEnumerable<string> EnumerateFiles(string directoryPath)
    {
        if (_enumerateFilesExceptions.TryGetValue(directoryPath, out Exception? exception))
        {
            throw exception;
        }

        return _filesByDirectory.TryGetValue(directoryPath, out List<string>? files) ? files : Enumerable.Empty<string>();
    }

    public IEnumerable<string> EnumerateDirectories(string directoryPath) =>
        _subdirectoriesByDirectory.TryGetValue(directoryPath, out List<string>? dirs) ? dirs : Enumerable.Empty<string>();

    public FileMetadata GetFileMetadata(string filePath) => _metadataByPath[filePath];

    public Stream OpenRead(string filePath) =>
        new MemoryStream(_contentByPath.TryGetValue(filePath, out byte[]? content) ? content : Array.Empty<byte>(), writable: false);
}
