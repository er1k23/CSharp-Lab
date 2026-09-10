using Npgsql;
using SocialNetwork.Models;

namespace SocialNetwork.Services;

public class UserService
{
    public void Register()
    {
        Console.Write("Username: ");
        string? username = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(username))
        {
            Console.WriteLine("Username can't be empty !");
            return;
        }
        
        Console.Write("Password: ");
        string? password = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(password))
        {
            Console.WriteLine("Password can't be empty !");
            return;
        }
        
        string passwordHash = PasswordHasher.Hash(password);

        
        Console.Write("First Name: ");
        string? firstName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(firstName))
        {
            Console.WriteLine("First Name can't be empty !");
            return;
        }
        
        Console.Write("Last Name: ");
        string? lastName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(lastName))
        {
            Console.WriteLine("Last Name can't be empty !");
            return;
        }
        
        Console.Write("Date of birth: ");
        string? dateOfBirth = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(dateOfBirth))
        {
            Console.WriteLine("Date of birth can't be empty !");
            return;
        }

        User user = new User
        {
            Username = username,
            PasswordHash = passwordHash,
            FirstName = firstName,
            LastName = lastName,
            DateOfBirth = dateOfBirth
        };
        
        try
        {
            Database.InsertUser(
                user.Username,
                user.PasswordHash,
                user.FirstName,
                user.LastName,
                user.DateOfBirth);
            
            Console.WriteLine("\n The user was created successfully!");
        }
        catch (PostgresException ex)
        {
            if (ex.SqlState == "23505" &&
                ex.ConstraintName == "users_username_key")
            {
                Console.WriteLine("Username is already taken!");
            }
            else
            {
                throw;
            }
        }
    }
}