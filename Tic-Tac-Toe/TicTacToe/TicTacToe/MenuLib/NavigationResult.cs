namespace TicTacToe.MenuLib;

public class NavigationResult
{
    public NavigationResultType Type { get; }
    public Menu? Menu { get; }

    private NavigationResult(NavigationResultType type, Menu? menu = null)
    {
        Type = type;
        Menu = menu;
    }

    public static NavigationResult None() => new NavigationResult(NavigationResultType.None);
    public static NavigationResult GoTo(Menu next) => new NavigationResult(NavigationResultType.GoTo, next);
    public static NavigationResult Wait() => new NavigationResult(NavigationResultType.Wait);
    public static NavigationResult Back() => new NavigationResult(NavigationResultType.Back);
    public static NavigationResult Exit() => new NavigationResult(NavigationResultType.Exit);
}