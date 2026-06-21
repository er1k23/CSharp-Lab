namespace TicTacToe.Core;

public static class UsernamePrompt
{
    public static void Run()
    {
        Console.Clear();
        Console.WriteLine("=== Welcome to Tic-Tac-Toe ===");

        string? input;
        do
        {
            Console.Write("Enter your username: ");
            input = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Username cannot be empty. Try again.");
            }
        } while (string.IsNullOrWhiteSpace(input));
        
        UserSession.SetUsername(input);
    }
}