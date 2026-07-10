using System.IO;

namespace File_IO_Exercises.Ex02_InboxScanner;

public class InboxScanner
{
    public static void Run()
    {
        string directoryName = "data/inbox";

        try
        {

            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            int fileCount = 0;
            foreach (var file in Directory.EnumerateFiles(directoryName))
            {
                Console.WriteLine($"File name is : {Path.GetFileName(file)}");
                FileInfo info = new FileInfo(file);
                Console.WriteLine($"Size: {info.Length} bytes");
                fileCount++;
            }
            
            Console.WriteLine($"Total count at the end is : {fileCount}");
        }
        
        catch (DirectoryNotFoundException error)
        {
            Console.WriteLine($"Папка не найдена: {error.Message}");
        }
        
        catch (UnauthorizedAccessException error)
        {
            Console.WriteLine($"Нет прав доступа: {error.Message}");
        }
        
        catch (IOException error)
        {
            Console.WriteLine($"Ошибка ввода/вывода: {error.Message}");
        }
    }
}