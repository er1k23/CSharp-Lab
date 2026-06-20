using TicTacToe.MenuImplementation;
using TicTacToe.MenuLib;

namespace TicTacToe;

class Program
{
    static void Main(string[] args)
    {
        MenuRunner.Run(new MainMenu());
    }
}