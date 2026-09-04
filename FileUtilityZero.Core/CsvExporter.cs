using System.Text;

namespace FileUtilityZero.Core;

// Instance-based replacement for the old static FileAccess CSV-writing
// methods (ExportDataTableToCSV/EscapeCsvField/BuildCsvLine), now operating
// on FileScanResult instead of a DataTable.
public sealed class CsvExporter
{
    // All twelve columns are always present in the header and every row,
    // even for scans run without IncludeHash/IncludeCategory - File Hash and
    // Category are simply left as blank cells for those rows (see BuildLine
    // below), so every export file has the same shape regardless of which
    // ScanOptions were used to produce it.
    private static readonly string[] Headers =
    {
        "File Name", "File Path", "File Size", "Creation Time", "Last Write Time", "Last Access Time",
        "Extension", "Attributes", "Is Read Only", "Directory Name", "File Hash", "Category"
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
        result.LastAccessTime.ToString(),
        result.Extension,
        result.Attributes.ToString(),
        result.IsReadOnly.ToString(),
        result.DirectoryName,
        // FileHash/Category are null when the scan didn't opt into them.
        // BuildLine takes string?[] and EscapeField treats null as
        // string.Empty, so these naturally render as a blank ("") cell
        // rather than being omitted.
        result.FileHash,
        result.Category);

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
