using Npgsql;
using SocialNetwork.Models;
using SocialNetwork;
using SocialNetwork.Services;

namespace SocialNetwork;

class Program
{
    static void Main(string[] args)
    {
        UserService userService = new UserService();
        
        userService.Register();
    }
}