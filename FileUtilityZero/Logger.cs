using System;
using System.IO;

public class Logger
{
    private static string logFilePath;

    public Logger(string FilePath)
    {
        logFilePath = FilePath;
    }

    public static void Log(string message)
    {
        try
        {
            // Ensure the directory exists
            string directoryPath = Path.GetDirectoryName(logFilePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // Append text to the log file, creating the file if it does not exist
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while logging: {ex.Message}");
        }
    }
}
