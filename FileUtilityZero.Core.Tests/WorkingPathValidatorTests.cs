namespace FileUtilityZero.Core.Tests;

public class WorkingPathValidatorTests
{
    [Theory]
    [InlineData(@"\\server\share")]
    [InlineData(@"\\server\share\subfolder")]
    [InlineData(@"  \\server\share")]
    public void IsUncPath_UncSyntax_ReturnsTrue(string path)
    {
        Assert.True(WorkingPathValidator.IsUncPath(path));
    }

    [Theory]
    [InlineData(@"C:\Users\me\Documents")]
    [InlineData(@"Z:\MappedDrive\Data")]
    [InlineData(@"\singlebackslash")]
    [InlineData("")]
    public void IsUncPath_LocalOrMappedPath_ReturnsFalse(string path)
    {
        Assert.False(WorkingPathValidator.IsUncPath(path));
    }
}
