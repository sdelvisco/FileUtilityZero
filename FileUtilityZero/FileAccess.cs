using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Text;

namespace FileUtilityZero
{
    public class FileAccess
    {
        public static DataSet filesDS = new DataSet();
        public static DataTable filesDT = CreateFilesDataTable();

        public static int GetFiles(string rootDirectory)
        {
            try
            {
                // Create a new DataTable to store the files
                filesDT = CreateFilesDataTable();
                filesDS.Tables.Add(filesDT);

                Thread thread = new(() => ScanDirectory(rootDirectory, filesDT));
                thread.Start();
                thread.Join();

                return filesDT.Rows.Count;
            }
            catch (Exception ex)
            {
                Logger.Log($"An error occurred while scanning directory {rootDirectory}: {ex.Message}");
                return 0;
            }
        }

        private static DataTable CreateFilesDataTable()
        {
            DataTable table = new("Files");
            table.Columns.Add("FileName", typeof(string));
            table.Columns.Add("LastAccessTime", typeof(DateTime));
            table.Columns.Add("FilePath", typeof(string));
            table.Columns.Add("FileSize", typeof(long));
            table.Columns.Add("CreationTime", typeof(DateTime));
            table.Columns.Add("LastWriteTime", typeof(DateTime));

            return table;
        }

        private static void ScanDirectory(string directoryPath, DataTable filesTable)
        {
            try
            {
                // Get all files in the current directory
                foreach (var filePath in Directory.GetFiles(directoryPath))
                {
                    FileInfo fileInfo = new(filePath);
                    DataRow newRow = filesTable.NewRow();
                    newRow["FileName"] = Path.GetFileName(fileInfo.FullName);
                    newRow["LastAccessTime"] = fileInfo.LastAccessTime;
                    newRow["FilePath"] = fileInfo.FullName;
                    newRow["FileSize"] = fileInfo.Length;
                    newRow["CreationTime"] = fileInfo.CreationTime;
                    newRow["LastWriteTime"] = fileInfo.LastWriteTime;
                    filesTable.Rows.Add(newRow);
                }

                // Recursively scan each subdirectory
                foreach (var subDirPath in Directory.GetDirectories(directoryPath))
                {
                    ScanDirectory(subDirPath, filesTable);
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"An error occurred while scanning directory {directoryPath}: {ex.Message}");
            }
        }

        public static void ExportDataTableToCSV(DataTable dataTable, string filePath)
        {
            StringBuilder csvData = new();

            // Add the headers
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                csvData.Append(dataTable.Columns[i].ColumnName);
                if (i < dataTable.Columns.Count - 1)
                    csvData.Append(",");
            }
            csvData.AppendLine();

            // Add the data
            foreach (DataRow row in dataTable.Rows)
            {
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    // Handle commas in data
                    string data = row[i].ToString().Replace(",", ";");
                    csvData.Append(data);
                    if (i < dataTable.Columns.Count - 1)
                        csvData.Append(",");
                }
                csvData.AppendLine();
            }

            // Write to file
            File.WriteAllText(filePath, csvData.ToString());
        }
    }

    public class FileAccessInfo
    {
        public string? FileName { get; set; }
        public string? LastAccessTime { get; set; }
        public string? FilePath { get; set; }
        public string? FileSize { get; set; }
        public string? CreationTime { get; set; }
        public string? LastWriteTime { get; set; }
    }
}