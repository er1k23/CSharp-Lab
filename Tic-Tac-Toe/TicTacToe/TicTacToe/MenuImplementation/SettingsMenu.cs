using TicTacToe.MenuLib;

namespace TicTacToe.MenuImplementation;

public class SettingsMenu : Menu
{
    public SettingsMenu() : base("Settings Menu")
    {
        ConfigureOptionSize(0);
    }

    protected override NavigationResult HandleOption(string option)
    {
        return NavigationResult.None();
    }
}