using File_IO_Exercises.Ex01_DailyReportArchiver;
using File_IO_Exercises.Ex02_InboxScanner;
using File_IO_Exercises.Ex03_PartialBinaryDownloader;
using File_IO_Exercises.Ex04_Utf8LogProcessor;

namespace File_IO_Exercises;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Running the Main function....");
        
        //~~~EX-1
        // DailyReportArchiver.Run();
        
        //~~~EX-2
        // InboxScanner.Run();
        
        //~~~EX-3
        // PartialBinaryDownloader.Run();
        
        //~~~EX-4
        Utf8LogProcessor.Run();
    }
}