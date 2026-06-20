using TicTacToe.MenuLib;

namespace TicTacToe.MenuImplementation;

public class PlayMenu : Menu
{
    public PlayMenu() : base("Play Menu")
    {
        ConfigureOptionSize(1);
        AddOption("1", "Say Hello");
    }

    protected override NavigationResult HandleOption(string option)
    {
        switch (option)
        {
            case "1":
                Console.WriteLine("Hello World!");
                return NavigationResult.Wait();
        }

        return NavigationResult.None();
    }
}