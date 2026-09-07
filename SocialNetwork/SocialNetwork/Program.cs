using Npgsql;

namespace SocialNetwork;

class Program
{
    static void Main(string[] args)
    {
        Database.EnsureSchema();
        Console.WriteLine("User table is created!");
    }
}