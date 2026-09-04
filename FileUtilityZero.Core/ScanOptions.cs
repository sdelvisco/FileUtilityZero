namespace FileUtilityZero.Core;

// Toggles for the optional, expensive FileScanResult fields (FileHash reads
// a file's full contents to hash it; Category is cheap on its own but is
// grouped here since it's the other opt-in field). Both default to false so
// FileScanner.Scan stays a cheap, metadata-only scan unless the caller
// explicitly asks for more.
public sealed record ScanOptions(
    bool IncludeHash = false,
    bool IncludeCategory = false);
