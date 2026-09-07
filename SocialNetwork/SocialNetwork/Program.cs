using Npgsql;

namespace SocialNetwork;

class Program
{
    static void Main(string[] args)
    {
        Database.EnsureSchema();
        
        Database.InsertUser(            
            "erik",
            "test_hash",
            "Erik",
            "Rumian",
            "2004-01-01");
        Console.WriteLine("Everything Work");
    }
}