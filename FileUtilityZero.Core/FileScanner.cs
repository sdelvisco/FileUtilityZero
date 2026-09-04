namespace FileUtilityZero.Core;

// Instance-based, injectable replacement for the old static FileAccess class.
// Depends on IFileSystem instead of System.IO directly, so scans can be
// tested against a fake directory tree without touching a real disk.
public sealed class FileScanner
{
    private readonly IFileSystem _fileSystem;
    private readonly ILogger _logger;

    public FileScanner(IFileSystem fileSystem, ILogger logger)
    {
        _fileSystem = fileSystem;
        _logger = logger;
    }

    public List<FileScanResult> Scan(string rootDirectory)
    {
        List<FileScanResult> results = new();

        try
        {
            ScanDirectory(rootDirectory, results);
        }
        catch (Exception ex)
        {
            _logger.Log($"An error occurred while scanning directory {rootDirectory}: {ex.Message}");
        }

        return results;
    }

    private void ScanDirectory(string directoryPath, List<FileScanResult> results)
    {
        try
        {
            foreach (string filePath in _fileSystem.EnumerateFiles(directoryPath))
            {
                FileMetadata metadata = _fileSystem.GetFileMetadata(filePath);
                results.Add(new FileScanResult(
                    metadata.Name,
                    metadata.FullPath,
                    metadata.Length,
                    metadata.CreationTime,
                    metadata.LastWriteTime,
                    metadata.LastAccessTime,
                    Extension: Path.GetExtension(metadata.FullPath),
                    Attributes: metadata.Attributes,
                    IsReadOnly: metadata.Attributes.HasFlag(FileAttributes.ReadOnly),
                    DirectoryName: Path.GetDirectoryName(metadata.FullPath) ?? string.Empty));
            }

            foreach (string subDirPath in _fileSystem.EnumerateDirectories(directoryPath))
            {
                ScanDirectory(subDirPath, results);
            }
        }
        catch (Exception ex)
        {
            _logger.Log($"An error occurred while scanning directory {directoryPath}: {ex.Message}");
        }
    }
}
