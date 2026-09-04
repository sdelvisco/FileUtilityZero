using System.Text;
using FileUtilityZero.Core.Tests.TestDoubles;

namespace FileUtilityZero.Core.Tests;

public class FileHasherTests
{
    // Known-answer SHA-256 test vectors, independently verified with
    // `sha256sum` rather than typed from memory, so a transcription slip
    // here can't silently validate a broken hasher.
    [Theory]
    [InlineData("abc", "ba7816bf8f01cfea414140de5dae2223b00361a396177a9cb410ff61f20015ad")]
    [InlineData("", "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855")]
    [InlineData("hello world", "b94d27b9934d3e08a52e52d7da7dabfac484efe37a5380ee9088f7ace2efcde9")]
    public void ComputeSha256Hex_KnownContent_ReturnsKnownHash(string content, string expectedHash)
    {
        FakeFileSystem fileSystem = new();
        string filePath = fileSystem.AddFile("root", "file.txt", content: Encoding.UTF8.GetBytes(content));
        FileHasher hasher = new(fileSystem);

        string actual = hasher.ComputeSha256Hex(filePath);

        Assert.Equal(expectedHash, actual);
    }

    [Fact]
    public void ComputeSha256Hex_ReturnsLowercaseHex()
    {
        FakeFileSystem fileSystem = new();
        string filePath = fileSystem.AddFile("root", "file.bin", content: new byte[] { 0xAB, 0xCD, 0xEF });
        FileHasher hasher = new(fileSystem);

        string actual = hasher.ComputeSha256Hex(filePath);

        Assert.Equal(actual, actual.ToLowerInvariant());
        Assert.Equal(64, actual.Length);
    }

    [Fact]
    public void ComputeSha256Hex_DifferentContent_ProducesDifferentHashes()
    {
        FakeFileSystem fileSystem = new();
        string filePathA = fileSystem.AddFile("root", "a.txt", content: Encoding.UTF8.GetBytes("content A"));
        string filePathB = fileSystem.AddFile("root", "b.txt", content: Encoding.UTF8.GetBytes("content B"));
        FileHasher hasher = new(fileSystem);

        string hashA = hasher.ComputeSha256Hex(filePathA);
        string hashB = hasher.ComputeSha256Hex(filePathB);

        Assert.NotEqual(hashA, hashB);
    }
}
