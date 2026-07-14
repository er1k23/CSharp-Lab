namespace File_IO_Exercises.Ex03_PartialBinaryDownloader;
using System.Text;
using System.IO;


public class PartialBinaryDownloader
{
    public static void Run()
    {
        string downloadsFolder = "data/downloads";
        string filePath = Path.Combine(downloadsFolder, "target.bin");

        try
        {

            if (!Directory.Exists(downloadsFolder))
            {
                Directory.CreateDirectory((downloadsFolder));
            }

            byte[] blockA = Encoding.UTF8.GetBytes("This is Block A content");
            byte[] blockB = Encoding.UTF8.GetBytes("This is Block B content");

            using (FileStream fsWrite = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                fsWrite.Position = 0;
                fsWrite.Write(blockA, 0, blockA.Length);

                fsWrite.Position = 1024;
                fsWrite.Write(blockB, 0, blockB.Length);
            }

            using (FileStream fsRead = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                byte[] bufferA = new byte[blockA.Length];
                fsRead.Position = 0;
                fsRead.ReadExactly(bufferA, 0, bufferA.Length);
                string readBackA = Encoding.UTF8.GetString(bufferA);

                byte[] bufferB = new byte[blockB.Length];
                fsRead.Position = 1024;
                fsRead.ReadExactly(bufferB, 0, bufferB.Length);
                string readBackB = Encoding.UTF8.GetString(bufferB);

                string originalA = Encoding.UTF8.GetString(blockA);
                string originalB = Encoding.UTF8.GetString(blockB);


                if (readBackA == originalA && readBackB == originalB)
                {
                    Console.WriteLine("Both blocks verified OK");
                    Console.WriteLine($"Block A: {readBackA}");
                    Console.WriteLine($"Block B: {readBackB}");
                }
                else
                {
                    Console.WriteLine("Verification FAILED");
                }
            }
        }
        catch (DirectoryNotFoundException error)
        {
            Console.WriteLine($"Directory Not Found {error.Message}");
        }
        catch (UnauthorizedAccessException error)
        {
            Console.WriteLine($"Unauthorized Access {error.Message}");
        }
        catch (IOException error)
        {
            Console.WriteLine($"Exception Input or Output {error.Message}");
        }
    }
}