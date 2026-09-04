namespace FileUtilityZero.Core;

// Metadata IFileSystem returns for a single file path.
//
// Attributes defaults to `default` (no flags set) so existing test-double
// construction that doesn't care about it keeps compiling unchanged.
public sealed record FileMetadata(
    string Name,
    string FullPath,
    long Length,
    DateTime CreationTime,
    DateTime LastWriteTime,
    DateTime LastAccessTime,
    FileAttributes Attributes = default);
