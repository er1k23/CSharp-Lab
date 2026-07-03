using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        var cache = new Cache<string>(TimeSpan.FromSeconds(5));

        cache.Set("user_1", "Erik");
        cache.Set("user_2", "Arman");

        Console.WriteLine(cache.Get("user_1"));
        Console.WriteLine(cache.Get("user_2"));
        
        Console.WriteLine(cache.IsExpired("user_1"));

        Console.WriteLine("Wait 6 sec ...");
        Thread.Sleep(6000);
        
        
        Console.WriteLine(cache.Get("user_1"));
        
        Console.WriteLine(cache.IsExpired("user_2"));
    }
}