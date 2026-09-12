using Library.Pg.Data;
using Library.Pg.Models;
using Library.Pg.Queries;
using Library.Pg.Operations;

namespace Library.Pg;

class Program
{
    static void Main(string[] args)
    {

        using var context = new LibraryContext();

        var canConnect = context.Database.CanConnect();

        Console.WriteLine($"Database connection: {canConnect}");
        
        DbInitializer.Seed(context);
        
        // BookQueries.RunAll(context);
        
        BookOperations.RunAll(context);
    }
}