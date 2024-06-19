using System;
using System.IO;
using System.Linq;

public class clsFileUtility
{
    public clsFileUtility()
    {
        public static string GetNewestFileInDirectory(string directoryPath)
        {
            DirectoryInfo directory = new DirectoryInfo(directoryPath);

            // Ensure the directory exists
            if (!directory.Exists)
            {
                throw new DirectoryNotFoundException($"The specified directory was not found: {directoryPath}");
            }

            // Get all files in the directory, ordered by creation time in descending order
            FileInfo[] files = directory.GetFiles().OrderByDescending(f => f.CreationTime).ToArray();

            // Ensure there is at least one file in the directory
            if (files.Length == 0)
            {
                return "No files found in the directory.";
            }

            // Return the full path of the newest file
            return files[0].FullName;
        }
    }
}
