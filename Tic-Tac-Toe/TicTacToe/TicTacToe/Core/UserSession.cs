namespace TicTacToe.Core;

public static class UserSession
{
    public static string Username { get; private set; } = string.Empty;

    public static void SetUsername(string username)
    {
        Username = username;
    }

    public static bool HasUsername()
    {
        return !string.IsNullOrWhiteSpace(Username);
    }
    
}