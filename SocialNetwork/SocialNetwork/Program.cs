using Npgsql;

namespace SocialNetwork;

class Program
{
    static void Main(string[] args)
    {
        Database.EnsureSchema();
        
        // Database.InsertUser(            
        //     "bob",
        //     "test_hash2",
        //     "Bob",
        //     "Smith",
        //     "1994-03-11");
        
        // Database.GetUsers();
        
        Database.GetUserById(1);
            
        Console.WriteLine("Everything Work");
        
    }
}