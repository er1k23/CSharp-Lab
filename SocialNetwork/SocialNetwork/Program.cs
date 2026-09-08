using Npgsql;

namespace SocialNetwork;

class Program
{
    static void Main(string[] args)
    {
        // Database.EnsureSchema();

        // Database.InsertUser(            
        //     "odyssey",
        //     "pas123",
        //     "Matt",
        //     "Damon",
        //     "1970-10-11");

        // Database.GetUsers();
        
        
        // Database.GetUserById(1);

        // Database.GetUserById(4);
        // Database.UpdateUser(userId:4,firstName:"Tom",lastName:"Holland");
        // Database.GetUserById(4);

        
        // Database.DeleteUser(3);
        
        
        // int? userId = Database.GetUserIdByUsername("bob");
        //
        // Console.WriteLine($"Bob ID: {userId}");
        
        // Database.AddFriendshipPair(1, 4);

        
        
        // Database.EnsureSchema();
        //
        // int currentUsaerid = 1;
        //
        // string friendUsername = "odyssey";
        //
        // int? friendUserId = Database.GetUserIdByUsername(friendUsername);
        //
        // if (friendUserId == null)
        // {
        //     Console.WriteLine("User not found");
        //     return;
        // }
        // else { Database.AddFriendshipPair(currentUsaerid,friendUserId.Value);}
        //
        // Console.WriteLine("Friendship created");
        //
        // Console.WriteLine("Project successfully working");
        
        
        Database.ListFriends(1);
    }
}