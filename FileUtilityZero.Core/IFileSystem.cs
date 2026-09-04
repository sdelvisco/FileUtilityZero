namespace FileUtilityZero.Core;

// Narrow abstraction over the exact filesystem operations FileScanner needs,
// so scanning can be unit tested against a fake directory tree instead of disk.
public interface IFileSystem
{
    bool DirectoryExists(string path);

    void CreateDirectory(string path);

    IEnumerable<string> EnumerateFiles(string directoryPath);

    IEnumerable<string> EnumerateDirectories(string directoryPath);

    FileMetadata GetFileMetadata(string filePath);
}
