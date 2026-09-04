namespace FileUtilityZero.Core;

// Plain, platform-agnostic replacement for the old DataTable row shape.
public sealed record FileScanResult(
    string FileName,
    string FilePath,
    long FileSize,
    DateTime CreationTime,
    DateTime LastWriteTime,
    DateTime LastAccessTime);
