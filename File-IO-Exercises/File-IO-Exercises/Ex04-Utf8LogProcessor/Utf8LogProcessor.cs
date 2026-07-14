namespace File_IO_Exercises.Ex04_Utf8LogProcessor;

using System.IO;
using System.Text;

public class Utf8LogProcessor
{
    public static void Run()
    {
        string logsFolder = "data/logs";
        string filePath = Path.Combine(logsFolder, "app.log");

        try
        {
            if (!Directory.Exists(logsFolder))
            {
                Directory.CreateDirectory(logsFolder);
            }

            using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                writer.WriteLine("2026-07-11 10:00:00 INFO Application started");
                writer.WriteLine("2026-07-11 10:00:05 ERROR Connection failed");
                writer.WriteLine("2026-07-11 10:00:10 INFO Retry attempt 1");
                writer.WriteLine("2026-07-11 10:00:15 ERROR Timeout occurred");
                writer.WriteLine("2026-07-11 10:00:20 INFO Обработка запроса завершена успешно");
            }

            int errorCount = 0;

            using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine($"Read line: {line}");

                    if (line.Contains("ERROR"))
                    {
                        errorCount++;
                    }
                }
            }

            Console.WriteLine($"Error count: {errorCount}");
        }
        catch (DirectoryNotFoundException error)
        {
            Console.WriteLine($"Directory Not Found: {error.Message}");
        }
        catch (UnauthorizedAccessException error)
        {
            Console.WriteLine($"Unauthorized Access: {error.Message}");
        }
        catch (IOException error)
        {
            Console.WriteLine($"IO Exception: {error.Message}");
        }
    }
}