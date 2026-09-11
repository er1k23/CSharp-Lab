using Library.Pg.Data;

namespace Library.Pg;

class Program
{
    static void Main(string[] args)
    {

        using var context = new LibraryContext();

        var canConnect = context.Database.CanConnect();

        Console.WriteLine($"Database connection: {canConnect}");
        
    }
}