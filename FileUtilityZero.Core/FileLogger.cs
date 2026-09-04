namespace FileUtilityZero.Core;

// Instance-based replacement for the old static Logger class. The log file
// path is supplied explicitly through the constructor instead of relying on
// a static field that was never actually initialized.
public sealed class FileLogger : ILogger
{
    private readonly string _logFilePath;

    public FileLogger(string logFilePath)
    {
        _logFilePath = logFilePath;
    }

    public void Log(string message)
    {
        try
        {
            string? directoryPath = Path.GetDirectoryName(_logFilePath);
            if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            using StreamWriter writer = new(_logFilePath, true);
            writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while logging: {ex.Message}");
        }
    }
}
