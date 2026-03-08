namespace AbstractionAndInterfaces;

public interface ILogger
{
    void LogError(string message);
    void LogInfo(string message);
}

public class FileLogger : ILogger
{
    private readonly string _filePath;

    public FileLogger(string filePath)
    {
        _filePath = filePath;
    }

    public void LogError(string message)
    {
        File.AppendAllText(_filePath, $"[ERROR] {DateTime.Now}: {message}{Environment.NewLine}");
        Console.WriteLine($"FileLogger wrote ERROR to {_filePath}");
    }

    public void LogInfo(string message)
    {
        File.AppendAllText(_filePath, $"[INFO]  {DateTime.Now}: {message}{Environment.NewLine}");
        Console.WriteLine($"FileLogger wrote INFO  to {_filePath}");
    }
}

public class ConsoleLogger : ILogger
{
    public void LogError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[ERROR] {message}");
        Console.ResetColor();
    }

    public void LogInfo(string message)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"[INFO]  {message}");
        Console.ResetColor();
    }
}
