namespace Generics_Exercises;

using Generics_Exercises.Models;
using Generics_Exercises.Algorithms;


class Program
{
    static void Main(string[] args)
    {
        
        // TASK #1 
        // var pair = new Pair<int, string>(7, "seven");
        // Console.WriteLine($"Original: {pair}");
        //
        // var swapped = pair.SwapSides();
        // Console.WriteLine($"Swapped: {swapped}");
        
        //-------------------------------------------------
        
        
        
        
        // TASK #2

        
        //-------------------------------------------------

        int[] numbers = { 1, 2, 3, 4, 5 };

        int[] evens = Collections.Filter<int>(numbers, n => n % 2 == 0);
        string[] labels = Collections.Project<int, string>(evens, n => $"N{n}");

        Console.WriteLine(string.Join(", ", labels));

    }
}

