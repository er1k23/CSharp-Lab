using Library.Pg.Data;
using Microsoft.EntityFrameworkCore;
using Library.Pg.Models;

namespace Library.Pg.Queries;

public static class BookBonusQueries
{
    public static void ShowQuerySql(LibraryContext context)
    {
        var query = context.Books.Where(book => !book.IsRead);
        
        Console.WriteLine("Generated SQL:");
        Console.WriteLine(query.ToQueryString());
    }


    public static void ShowNoTracking(LibraryContext context)
    {
        var books = context.Books.AsNoTracking().ToList();
        
        Console.WriteLine($"Loaded {books.Count} books with NoTracking.");
    }
    
    
    public static async Task ShowAsync(LibraryContext context)
    {
        var books = await context.Books.ToListAsync();

        Console.WriteLine($"Loaded {books.Count} books asynchronously.");
    }

    public static List<Book> Search(LibraryContext context, string searchTerm)
    {
        return context.Books
            .Where(book =>
                book.Title.Contains(searchTerm) ||
                book.Author.Contains(searchTerm))
            .ToList();
    } 
}