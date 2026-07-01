namespace Generics_Exercises;

using Generics_Exercises.Models;

class Program
{
    static void Main(string[] args)
    {
        var pair = new Pair<int, string>(7, "seven");
        Console.WriteLine($"Original: {pair}");

        var swapped = pair.SwapSides();
        Console.WriteLine($"Swapped: {swapped}");
    }
}