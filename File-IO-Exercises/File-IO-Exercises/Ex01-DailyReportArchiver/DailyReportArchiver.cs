namespace File_IO_Exercises.Ex01_DailyReportArchiver;
using System.IO;

public class DailyReportArchiver
{
    public static void Run()
    {
        string reportContent = "Line one\nLine two\nLine three";
        string reportsFolder = "data/reports";
        string filePath = Path.Combine("data", "reports", "daily-report.txt");

        try
        {
            if (!Directory.Exists(reportsFolder))
            {
                Directory.CreateDirectory(reportsFolder);
            }
            
            File.WriteAllText(filePath, reportContent);
            
            string readenContent = File.ReadAllText(filePath); // ← исправлено
            
            if (reportContent == readenContent)
            {
                Console.WriteLine("Saved OK");
            }
            else
            {
                Console.WriteLine("Not OK");
            }
        }
        catch (DirectoryNotFoundException ex)
        {
            Console.WriteLine($"Папка не найдена: {ex.Message}");
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"Нет прав доступа: {ex.Message}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Ошибка ввода/вывода: {ex.Message}");
        }
    }
}