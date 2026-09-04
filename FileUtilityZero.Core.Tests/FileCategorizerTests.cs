namespace FileUtilityZero.Core.Tests;

public class FileCategorizerTests
{
    [Theory]
    [InlineData(".cpp", "Code")]
    [InlineData(".cs", "Code")]
    [InlineData(".py", "Code")]
    [InlineData(".js", "Code")]
    [InlineData(".html", "Code")]
    [InlineData(".jpg", "Image")]
    [InlineData(".png", "Image")]
    [InlineData(".svg", "Image")]
    [InlineData(".pdf", "Document")]
    [InlineData(".docx", "Document")]
    [InlineData(".csv", "Document")]
    [InlineData(".json", "Document")]
    [InlineData(".mp3", "Audio")]
    [InlineData(".wav", "Audio")]
    [InlineData(".flac", "Audio")]
    [InlineData(".mp4", "Video")]
    [InlineData(".mkv", "Video")]
    [InlineData(".mov", "Video")]
    [InlineData(".zip", "Archive")]
    [InlineData(".tar", "Archive")]
    [InlineData(".iso", "Archive")]
    [InlineData(".exe", "Executable")]
    [InlineData(".dll", "Executable")]
    [InlineData(".apk", "Executable")]
    [InlineData(".ttf", "Font")]
    [InlineData(".woff2", "Font")]
    public void GetCategory_KnownExtension_ReturnsExpectedBucket(string extension, string expectedCategory)
    {
        string actual = FileCategorizer.GetCategory(extension);

        Assert.Equal(expectedCategory, actual);
    }

    [Theory]
    [InlineData(".xyz")]
    [InlineData(".doesnotexist")]
    [InlineData("")]
    [InlineData(".")]
    public void GetCategory_UnknownOrEmptyExtension_FallsBackToOther(string extension)
    {
        string actual = FileCategorizer.GetCategory(extension);

        Assert.Equal("Other", actual);
    }

    [Fact]
    public void GetCategory_IsCaseInsensitive()
    {
        Assert.Equal(FileCategorizer.GetCategory(".cpp"), FileCategorizer.GetCategory(".CPP"));
        Assert.Equal("Code", FileCategorizer.GetCategory(".CpP"));
    }
}
