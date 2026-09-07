using Npgsql;

namespace SocialNetwork;

class Program
{
    static void Main(string[] args)
    {
        using var connection = Database.Open();
        Console.WriteLine("Successfully connected to PostgreSQL!");
    }
}