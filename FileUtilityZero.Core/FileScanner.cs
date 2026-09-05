namespace FileUtilityZero.Core;

// Instance-based, injectable replacement for the old static FileAccess class.
// Depends on IFileSystem instead of System.IO directly, so scans can be
// tested against a fake directory tree without touching a real disk.
public sealed class FileScanner
{
    private readonly IFileSystem _fileSystem;
    private readonly ILogger _logger;
    private readonly FileHasher _fileHasher;

    public FileScanner(IFileSystem fileSystem, ILogger logger)
    {
        _fileSystem = fileSystem;
        _logger = logger;
        _fileHasher = new FileHasher(fileSystem);
    }

    // options defaults to a new ScanOptions() (IncludeHash/IncludeCategory
    // both false) when omitted, so existing callers keep getting the same
    // cheap, metadata-only scan they always did.
    //
    // progress, when supplied, is reported once per file as it's found -
    // this lets a caller running the scan on a background thread (eg via
    // Task.Run) stream results to a UI incrementally instead of waiting for
    // the whole tree to finish, without this class needing to know anything
    // about threads or synchronization contexts itself.
    public List<FileScanResult> Scan(string rootDirectory, ScanOptions? options = null, IProgress<FileScanResult>? progress = null)
    {
        options ??= new ScanOptions();
        List<FileScanResult> results = new();

        try
        {
            ScanDirectory(rootDirectory, results, options, progress);
        }
        catch (Exception ex)
        {
            _logger.Log($"An error occurred while scanning directory {rootDirectory}: {ex.Message}");
        }

        return results;
    }

    private void ScanDirectory(string directoryPath, List<FileScanResult> results, ScanOptions options, IProgress<FileScanResult>? progress)
    {
        try
        {
            foreach (string filePath in _fileSystem.EnumerateFiles(directoryPath))
            {
                FileMetadata metadata = _fileSystem.GetFileMetadata(filePath);
                string extension = Path.GetExtension(metadata.FullPath);

                // FileHash/Category are only computed when explicitly opted
                // into - hashing in particular reads the entire file, so it
                // stays skipped (null) unless IncludeHash is set.
                FileScanResult result = new(
                    metadata.Name,
                    metadata.FullPath,
                    metadata.Length,
                    metadata.CreationTime,
                    metadata.LastWriteTime,
                    metadata.LastAccessTime,
                    Extension: extension,
                    Attributes: metadata.Attributes,
                    IsReadOnly: metadata.Attributes.HasFlag(FileAttributes.ReadOnly),
                    DirectoryName: Path.GetDirectoryName(metadata.FullPath) ?? string.Empty,
                    FileHash: options.IncludeHash ? _fileHasher.ComputeSha256Hex(metadata.FullPath) : null,
                    Category: options.IncludeCategory ? FileCategorizer.GetCategory(extension) : null);

                results.Add(result);
                progress?.Report(result);
            }

            foreach (string subDirPath in _fileSystem.EnumerateDirectories(directoryPath))
            {
                ScanDirectory(subDirPath, results, options, progress);
            }
        }
        catch (Exception ex)
        {
            _logger.Log($"An error occurred while scanning directory {directoryPath}: {ex.Message}");
        }
    }
}
