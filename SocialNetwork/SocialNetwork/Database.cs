using Npgsql;

namespace SocialNetwork;

public class Database
{
    private const string ConnectionString =
        "Host=localhost;Port=5433;Database=social-network;Username=admin;Password=admin123";

    public static NpgsqlConnection Open()
    {
        var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        return connection;
    }
}