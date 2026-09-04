namespace FileUtilityZero.Core.Tests;

public class CsvExporterTests
{
    private readonly CsvExporter _exporter = new();

    [Theory]
    [InlineData("=cmd|'/c calc'!A1", "\"'=cmd|'/c calc'!A1\"")]
    [InlineData("+1+1", "\"'+1+1\"")]
    [InlineData("-1+1", "\"'-1+1\"")]
    [InlineData("@SUM(1)", "\"'@SUM(1)\"")]
    public void EscapeField_ValueStartingWithFormulaTrigger_IsPrefixedWithApostrophe(string input, string expected)
    {
        string actual = _exporter.EscapeField(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void EscapeField_PlainValue_IsNotPrefixed()
    {
        string actual = _exporter.EscapeField("report.txt");

        Assert.Equal("\"report.txt\"", actual);
    }

    [Fact]
    public void EscapeField_FormulaTriggerInsideValue_NotAtStart_IsNotPrefixed()
    {
        // Only a leading =, +, -, or @ makes a spreadsheet app treat the cell as
        // a formula - the same character elsewhere in the value is harmless.
        string actual = _exporter.EscapeField("report=final.txt");

        Assert.Equal("\"report=final.txt\"", actual);
    }

    [Fact]
    public void EscapeField_EmbeddedComma_IsPreservedInsideQuotes()
    {
        string actual = _exporter.EscapeField("a,b");

        Assert.Equal("\"a,b\"", actual);
    }

    [Fact]
    public void EscapeField_EmbeddedDoubleQuote_IsDoubled()
    {
        string actual = _exporter.EscapeField("She said \"hi\"");

        Assert.Equal("\"She said \"\"hi\"\"\"", actual);
    }

    [Fact]
    public void EscapeField_EmbeddedNewline_IsPreservedInsideQuotes()
    {
        string actual = _exporter.EscapeField("line1\nline2");

        Assert.Equal("\"line1\nline2\"", actual);
    }

    [Fact]
    public void EscapeField_Null_IsTreatedAsEmptyString()
    {
        string actual = _exporter.EscapeField(null);

        Assert.Equal("\"\"", actual);
    }

    [Fact]
    public void BuildLine_JoinsEscapedFieldsWithCommas()
    {
        string actual = _exporter.BuildLine("a", "b,c", "=evil");

        Assert.Equal("\"a\",\"b,c\",\"'=evil\"", actual);
    }

    [Fact]
    public void BuildHeaderLine_ReturnsExpectedSixColumns()
    {
        string actual = _exporter.BuildHeaderLine();

        Assert.Equal(
            "\"File Name\",\"File Path\",\"File Size\",\"Creation Time\",\"Last Write Time\",\"Last Access Time\"",
            actual);
    }

    [Fact]
    public void BuildLine_ForFileScanResult_EscapesAMaliciousFileName()
    {
        FileScanResult result = new(
            "=cmd|'/c calc'!A1.txt",
            "C:\\data\\=cmd|'/c calc'!A1.txt",
            100,
            new DateTime(2024, 1, 1),
            new DateTime(2024, 1, 2),
            new DateTime(2024, 1, 3));

        string actual = _exporter.BuildLine(result);

        Assert.StartsWith("\"'=cmd", actual);
    }

    [Fact]
    public void Export_WritesHeaderAndRowsToFile()
    {
        List<FileScanResult> results = new()
        {
            new FileScanResult("a.txt", "C:\\root\\a.txt", 10, new DateTime(2024, 1, 1), new DateTime(2024, 1, 1), new DateTime(2024, 1, 1)),
            new FileScanResult("=evil.txt", "C:\\root\\=evil.txt", 20, new DateTime(2024, 1, 1), new DateTime(2024, 1, 1), new DateTime(2024, 1, 1)),
        };
        string tempFile = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".csv");

        try
        {
            _exporter.Export(results, tempFile);
            string[] lines = File.ReadAllLines(tempFile);

            Assert.Equal(3, lines.Length);
            Assert.Equal(_exporter.BuildHeaderLine(), lines[0]);
            Assert.Contains("\"a.txt\"", lines[1]);
            Assert.Contains("\"'=evil.txt\"", lines[2]);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }
}
