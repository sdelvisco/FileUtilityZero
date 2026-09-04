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

    // Opens a file's full contents for reading. Added for FileHasher, so
    // hashing goes through this abstraction instead of touching System.IO
    // directly - the caller is responsible for disposing the returned stream.
    Stream OpenRead(string filePath);
}
