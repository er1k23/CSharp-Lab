using TicTacToe.MenuLib;

namespace TicTacToe.MenuImplementation;

public class AboutMenu : Menu
{
    public AboutMenu() : base("About Menu")
    {
    }

    protected override void InternalDisplay()
    {
        Console.WriteLine("Tic-Tac-Toe");
        Console.WriteLine("Author: Erik");
        Console.WriteLine("Course: TechGen C#");
        Console.WriteLine("Year: 2026");
    }

    protected override NavigationResult HandleOption(string option)
    {
        return NavigationResult.None();
    }
}