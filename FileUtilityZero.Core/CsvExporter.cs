using System.Text;

namespace FileUtilityZero.Core;

// Instance-based replacement for the old static FileAccess CSV-writing
// methods (ExportDataTableToCSV/EscapeCsvField/BuildCsvLine), now operating
// on FileScanResult instead of a DataTable.
public sealed class CsvExporter
{
    private static readonly string[] Headers =
    {
        "File Name", "File Path", "File Size", "Creation Time", "Last Write Time", "Last Access Time"
    };

    // Quotes a single CSV field per RFC 4180 (doubling any embedded quotes) and,
    // if the value starts with a character a spreadsheet app would interpret as
    // the start of a formula (=, +, -, @), prefixes it with an apostrophe so it
    // is opened as literal text instead of being evaluated (CSV/formula injection).
    public string EscapeField(string? value)
    {
        value ??= string.Empty;

        if (value.Length > 0 && (value[0] == '=' || value[0] == '+' || value[0] == '-' || value[0] == '@'))
        {
            value = "'" + value;
        }

        return "\"" + value.Replace("\"", "\"\"") + "\"";
    }

    // Builds one CSV line from a set of raw field values, escaping each field.
    public string BuildLine(params string?[] fields)
    {
        StringBuilder line = new();
        for (int i = 0; i < fields.Length; i++)
        {
            line.Append(EscapeField(fields[i]));
            if (i < fields.Length - 1)
                line.Append(",");
        }

        return line.ToString();
    }

    public string BuildHeaderLine() => BuildLine(Headers);

    public string BuildLine(FileScanResult result) => BuildLine(
        result.FileName,
        result.FilePath,
        result.FileSize.ToString(),
        result.CreationTime.ToString(),
        result.LastWriteTime.ToString(),
        result.LastAccessTime.ToString());

    public void Export(IEnumerable<FileScanResult> results, string filePath)
    {
        StringBuilder csvData = new();
        csvData.AppendLine(BuildHeaderLine());

        foreach (FileScanResult result in results)
        {
            csvData.AppendLine(BuildLine(result));
        }

        File.WriteAllText(filePath, csvData.ToString());
    }
}
