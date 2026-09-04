using System.Text;
using FileUtilityZero.Core.Tests.TestDoubles;

namespace FileUtilityZero.Core.Tests;

public class FileScannerTests
{
    [Fact]
    public void Scan_EmptyDirectory_ReturnsNoResults()
    {
        FakeFileSystem fileSystem = new();
        fileSystem.AddDirectory("root");
        FakeLogger logger = new();
        FileScanner scanner = new(fileSystem, logger);

        List<FileScanResult> results = scanner.Scan("root");

        Assert.Empty(results);
        Assert.Empty(logger.Messages);
    }

    [Fact]
    public void Scan_FilesInRoot_ReturnsMappedResults()
    {
        FakeFileSystem fileSystem = new();
        DateTime created = new(2024, 1, 1);
        DateTime written = new(2024, 1, 2);
        DateTime accessed = new(2024, 1, 3);
        fileSystem.AddFile("root", "a.txt", length: 123, creationTime: created, lastWriteTime: written, lastAccessTime: accessed);
        FileScanner scanner = new(fileSystem, new FakeLogger());

        List<FileScanResult> results = scanner.Scan("root");

        FileScanResult result = Assert.Single(results);
        Assert.Equal("a.txt", result.FileName);
        Assert.Equal("root/a.txt", result.FilePath);
        Assert.Equal(123, result.FileSize);
        Assert.Equal(created, result.CreationTime);
        Assert.Equal(written, result.LastWriteTime);
        Assert.Equal(accessed, result.LastAccessTime);
    }

    [Fact]
    public void Scan_NestedSubdirectories_RecursesAndCollectsAllFiles()
    {
        FakeFileSystem fileSystem = new();
        fileSystem.AddFile("root", "root.txt");
        fileSystem.AddSubdirectory("root", "root/child");
        fileSystem.AddFile("root/child", "child.txt");
        fileSystem.AddSubdirectory("root/child", "root/child/grandchild");
        fileSystem.AddFile("root/child/grandchild", "grandchild.txt");
        FileScanner scanner = new(fileSystem, new FakeLogger());

        List<FileScanResult> results = scanner.Scan("root");

        Assert.Equal(3, results.Count);
        Assert.Contains(results, r => r.FileName == "root.txt");
        Assert.Contains(results, r => r.FileName == "child.txt");
        Assert.Contains(results, r => r.FileName == "grandchild.txt");
    }

    [Fact]
    public void Scan_SubdirectoryThrows_LogsErrorAndStillReturnsSiblingResults()
    {
        FakeFileSystem fileSystem = new();
        fileSystem.AddSubdirectory("root", "root/broken");
        fileSystem.AddSubdirectory("root", "root/ok");
        fileSystem.AddFile("root/ok", "ok.txt");
        fileSystem.ThrowWhenEnumeratingFiles("root/broken", new UnauthorizedAccessException("access denied"));
        FakeLogger logger = new();
        FileScanner scanner = new(fileSystem, logger);

        List<FileScanResult> results = scanner.Scan("root");

        FileScanResult result = Assert.Single(results);
        Assert.Equal("ok.txt", result.FileName);
        Assert.Single(logger.Messages);
        Assert.Contains("root/broken", logger.Messages[0]);
    }

    [Fact]
    public void Scan_RootThrows_LogsErrorAndReturnsEmptyResults()
    {
        FakeFileSystem fileSystem = new();
        fileSystem.AddDirectory("root");
        fileSystem.ThrowWhenEnumeratingFiles("root", new DirectoryNotFoundException("gone"));
        FakeLogger logger = new();
        FileScanner scanner = new(fileSystem, logger);

        List<FileScanResult> results = scanner.Scan("root");

        Assert.Empty(results);
        Assert.Single(logger.Messages);
    }

    [Fact]
    public void Scan_PopulatesAlwaysIncludedFieldsRegardlessOfOptions()
    {
        FakeFileSystem fileSystem = new();
        fileSystem.AddSubdirectory("root", "root/sub");
        fileSystem.AddFile("root/sub", "a.cpp", attributes: FileAttributes.ReadOnly);
        FileScanner scanner = new(fileSystem, new FakeLogger());

        List<FileScanResult> results = scanner.Scan("root");

        FileScanResult result = Assert.Single(results);
        Assert.Equal(".cpp", result.Extension);
        Assert.Equal(FileAttributes.ReadOnly, result.Attributes);
        Assert.True(result.IsReadOnly);
        Assert.Equal("root/sub", result.DirectoryName);
    }

    [Fact]
    public void Scan_DefaultOptions_HashAndCategoryStayNull()
    {
        FakeFileSystem fileSystem = new();
        fileSystem.AddFile("root", "a.cpp", content: Encoding.UTF8.GetBytes("int main() {}"));
        FileScanner scanner = new(fileSystem, new FakeLogger());

        // No ScanOptions passed - should default to IncludeHash/IncludeCategory both false.
        List<FileScanResult> results = scanner.Scan("root");

        FileScanResult result = Assert.Single(results);
        Assert.Null(result.FileHash);
        Assert.Null(result.Category);
    }

    [Fact]
    public void Scan_OptionsBothFalse_HashAndCategoryStayNull()
    {
        FakeFileSystem fileSystem = new();
        fileSystem.AddFile("root", "a.cpp", content: Encoding.UTF8.GetBytes("int main() {}"));
        FileScanner scanner = new(fileSystem, new FakeLogger());

        List<FileScanResult> results = scanner.Scan("root", new ScanOptions(IncludeHash: false, IncludeCategory: false));

        FileScanResult result = Assert.Single(results);
        Assert.Null(result.FileHash);
        Assert.Null(result.Category);
    }

    [Fact]
    public void Scan_HashAndCategoryEnabled_PopulatesBothFieldsCorrectly()
    {
        FakeFileSystem fileSystem = new();
        byte[] content = Encoding.UTF8.GetBytes("int main() {}");
        fileSystem.AddFile("root", "a.cpp", content: content);
        FileScanner scanner = new(fileSystem, new FakeLogger());

        List<FileScanResult> results = scanner.Scan("root", new ScanOptions(IncludeHash: true, IncludeCategory: true));

        FileScanResult result = Assert.Single(results);
        Assert.Equal(new FileHasher(fileSystem).ComputeSha256Hex("root/a.cpp"), result.FileHash);
        Assert.Equal("Code", result.Category);
    }

    [Fact]
    public void Scan_OnlyIncludeHashEnabled_LeavesCategoryNull()
    {
        FakeFileSystem fileSystem = new();
        fileSystem.AddFile("root", "a.cpp", content: Encoding.UTF8.GetBytes("int main() {}"));
        FileScanner scanner = new(fileSystem, new FakeLogger());

        List<FileScanResult> results = scanner.Scan("root", new ScanOptions(IncludeHash: true, IncludeCategory: false));

        FileScanResult result = Assert.Single(results);
        Assert.NotNull(result.FileHash);
        Assert.Null(result.Category);
    }

    [Fact]
    public void Scan_OnlyIncludeCategoryEnabled_LeavesHashNull()
    {
        FakeFileSystem fileSystem = new();
        fileSystem.AddFile("root", "a.cpp", content: Encoding.UTF8.GetBytes("int main() {}"));
        FileScanner scanner = new(fileSystem, new FakeLogger());

        List<FileScanResult> results = scanner.Scan("root", new ScanOptions(IncludeHash: false, IncludeCategory: true));

        FileScanResult result = Assert.Single(results);
        Assert.Null(result.FileHash);
        Assert.Equal("Code", result.Category);
    }
}
