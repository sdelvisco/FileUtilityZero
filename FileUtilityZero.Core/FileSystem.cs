namespace FileUtilityZero.Core;

// Real IFileSystem implementation backed by System.IO. System.IO itself is
// fully cross-platform (unlike System.Windows.Forms), so this is safe to keep
// in Core - it's the concrete adapter that a UI project wires up by default.
public sealed class FileSystem : IFileSystem
{
    public bool DirectoryExists(string path) => Directory.Exists(path);

    public void CreateDirectory(string path) => Directory.CreateDirectory(path);

    public IEnumerable<string> EnumerateFiles(string directoryPath) => Directory.EnumerateFiles(directoryPath);

    public IEnumerable<string> EnumerateDirectories(string directoryPath) => Directory.EnumerateDirectories(directoryPath);

    public FileMetadata GetFileMetadata(string filePath)
    {
        FileInfo fileInfo = new(filePath);
        return new FileMetadata(
            fileInfo.Name,
            fileInfo.FullName,
            fileInfo.Length,
            fileInfo.CreationTime,
            fileInfo.LastWriteTime,
            fileInfo.LastAccessTime,
            fileInfo.Attributes);
    }
}
