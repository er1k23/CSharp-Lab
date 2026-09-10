namespace SocialNetwork.Models;

public class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string DateOfBirth { get; set; } = null!;
}