namespace FileUtilityZero.Core;

// Plain, platform-agnostic replacement for the old DataTable row shape.
//
// The trailing parameters below were appended (rather than modeled as a
// separate v2 record) so every existing positional `new FileScanResult(...)`
// call - in FileScanner and in the existing unit tests - keeps compiling
// unchanged; C# lets positional record callers omit trailing parameters that
// have defaults, same as a normal optional-parameter constructor.
//
// Extension/Attributes/IsReadOnly/DirectoryName are cheap, metadata-only
// values and are always populated by FileScanner. FileHash/Category are only
// computed when explicitly requested via ScanOptions (see FileScanner.Scan),
// since hashing reads full file contents and is comparatively expensive;
// they stay null otherwise.
//
// Attributes is stored as the raw System.IO.FileAttributes flags enum rather
// than a pre-formatted string: it keeps the full fidelity of the underlying
// flags for any programmatic consumer, and FileAttributes is a [Flags] enum
// whose default ToString() already renders a readable comma-separated list
// (e.g. "ReadOnly, Archive"), so CsvExporter can call .ToString() on it for
// a human-readable CSV cell without needing a second representation.
public sealed record FileScanResult(
    string FileName,
    string FilePath,
    long FileSize,
    DateTime CreationTime,
    DateTime LastWriteTime,
    DateTime LastAccessTime,
    string Extension = "",
    FileAttributes Attributes = default,
    bool IsReadOnly = false,
    string DirectoryName = "",
    string? FileHash = null,
    string? Category = null);
