using System.Security.Cryptography;

namespace FileUtilityZero.Core;

// Computes a SHA-256 hash of a file's full contents via IFileSystem, so
// scanning stays testable against a fake filesystem instead of real disk
// I/O - same pattern FileScanner already follows.
//
// This is the slow path of a scan (it reads every byte of every hashed
// file) and is deliberately kept simple for now: one synchronous stream
// read per call, no parallelism or async. That's out of scope for this
// stage per the task description; it can be revisited later if scan
// performance with hashing enabled turns out to matter.
public sealed class FileHasher
{
    private readonly IFileSystem _fileSystem;

    public FileHasher(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    // Returns the SHA-256 hash of the file at filePath as a 64-character
    // lowercase hex string.
    public string ComputeSha256Hex(string filePath)
    {
        using Stream stream = _fileSystem.OpenRead(filePath);
        byte[] hash = SHA256.HashData(stream);
        return Convert.ToHexStringLower(hash);
    }
}
