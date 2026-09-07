using Npgsql;

namespace SocialNetwork;

class Program
{
    static void Main(string[] args)
    {
        // Database.EnsureSchema();
        //
        // Database.InsertUser(            
        //     "odyssey",
        //     "pas123",
        //     "Matt",
        //     "Damon",
        //     "1970-10-11");
        //
        // Database.GetUsers();
        
        // Database.GetUserById(1);

        // Database.GetUserById(4);
        // Database.UpdateUser(userId:4,firstName:"Tom",lastName:"Holland");
        // Database.GetUserById(4);

        
        Database.DeleteUser(3);
        
        Console.WriteLine("Project successfully working");
        
    }
}