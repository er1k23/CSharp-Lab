using Npgsql;

namespace SocialNetwork;

class Program
{
    static void Main(string[] args)
    {
       
        Console.Write("Username: ");
        string? username = Console.ReadLine();
        
        Console.Write("Password: ");
        string? password = Console.ReadLine();
        string? passwordHash = PasswordHasher.Hash(password!);

        //
        // Console.Write("First Name: ");
        // string? firstName = Console.ReadLine();
        //
        // Console.Write("Last Name: ");
        // string? lastName = Console.ReadLine();
        //
        // Console.Write("Date of birth:: ");
        // string? dateOfBirth = Console.ReadLine();
        
        Console.WriteLine($"You entered: {username}");
        Console.WriteLine($"You entered: {password}");
        Console.WriteLine($"Hash: {passwordHash}");
        // Console.WriteLine($"You entered: {firstName}");
        // Console.WriteLine($"You entered: {lastName}");
        // Console.WriteLine($"You entered: {dateOfBirth}");

    }
}