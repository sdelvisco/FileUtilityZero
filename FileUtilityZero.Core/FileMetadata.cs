namespace FileUtilityZero.Core;

// Metadata IFileSystem returns for a single file path.
public sealed record FileMetadata(
    string Name,
    string FullPath,
    long Length,
    DateTime CreationTime,
    DateTime LastWriteTime,
    DateTime LastAccessTime);
