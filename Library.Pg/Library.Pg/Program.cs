using Library.Pg.Data;
using Library.Pg.Models;
using Library.Pg.Queries;
using Library.Pg.Operations;

namespace Library.Pg;

class Program
{
    static async Task Main(string[] args)
    {

        using var context = new LibraryContext();

        var canConnect = context.Database.CanConnect();

        Console.WriteLine($"Database connection: {canConnect}");
        
        // DbInitializer.Seed(context);
        
        // BookQueries.RunAll(context);
        
        // BookOperations.RunAll(context);
        // BookBonusQueries.ShowQuerySql(context);
        // BookBonusQueries.ShowNoTracking(context);
        // await BookBonusQueries.ShowAsync(context);

        // var searchResults = BookBonusQueries.Search(context, "Martin");
        //
        // Console.WriteLine("Search results:");
        // foreach (var book in searchResults)
        // {
        //     Console.WriteLine($"{book.Title} — {book.Author}");
        // }
    }
}