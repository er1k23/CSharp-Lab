using Npgsql;

namespace SocialNetwork;

class Program
{
    static void Main(string[] args)
    {
        string connectionString = "Host=localhost;Port=5433;Database=social-network;Username=admin;Password=admin123";

        using var connection = new NpgsqlConnection(connectionString);
        
        connection.Open();
        
        Console.WriteLine("Successfully connected to PostgreSQL!");
    }
}