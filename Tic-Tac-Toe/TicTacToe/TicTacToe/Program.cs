using TicTacToe.MenuImplementation;
using TicTacToe.MenuLib;
using TicTacToe.Core;

namespace TicTacToe;

class Program
{
    static void Main(string[] args)
    {
        if (!UserSession.HasUsername())
        {
            UsernamePrompt.Run();
        }
        
        MenuRunner.Run(new MainMenu());
    }
}