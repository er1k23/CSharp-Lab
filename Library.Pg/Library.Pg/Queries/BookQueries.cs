using Library.Pg.Data;
using Microsoft.EntityFrameworkCore;
namespace Library.Pg.Queries;

public class BookQueries
{
    public static void RunAll(LibraryContext context)
    {
        var allBooks = context.Books.ToList();

        foreach (var book in allBooks)
        {
            Console.WriteLine($"{book.BookId}: {book.Title} - {book.Author}");
        }

        
        var unreadBooks = context.Books.Where(book => !book.IsRead).ToList();

        foreach (var book in unreadBooks)
        {
            Console.WriteLine($"{book.BookId}: {book.Title}");
        }


        var fowlerBooks = context.Books
            .Where(book => book.Author == "Martin Fowler")
            .ToList();

        foreach (var book in fowlerBooks)
        {
            Console.WriteLine($"{book.BookId}: {book.Title} - {book.Author}");
        }

        
        var mostExpensiveBook = context.Books
            .OrderByDescending(book => book.Price)
            .First();

        Console.WriteLine(
            $"Most expensive: {mostExpensiveBook.Title} - {mostExpensiveBook.Price}"
        );


        var hasUnreadBooks = context.Books.Any(book => !book.IsRead);
        
        Console.WriteLine($"Has unread books: {hasUnreadBooks}");

        
        var unreadBooksCount = context.Books
            .Count(book => !book.IsRead);

        Console.WriteLine($"Unread books count: {unreadBooksCount}");

        
        var bookSummaries = context.Books.Select(book => new { book.Title, book.Price }).ToList();
        
        foreach (var book in bookSummaries)
        {
            Console.WriteLine($"{book.Title} - {book.Price}");
        }


        var pagedBooks = context.Books
            .OrderBy(book => book.BookId)
            .Skip(2)
            .Take(2)
            .ToList();

        foreach (var book in pagedBooks)
        {
            Console.WriteLine($"{book.BookId}: {book.Title}");
        }


        var bookByFind = context.Books.Find(1);

        Console.WriteLine(
            $"Find: {bookByFind?.Title}"
        );

        var bookByFirst = context.Books
            .First(book => book.BookId == 1);

        Console.WriteLine(
            $"First: {bookByFirst.Title}"
        );


        var totalPrice = context.Books
            .Sum(book => book.Price);

        Console.WriteLine($"Total price: {totalPrice}");

        
        var bookWithCode = context.Books.Where(book => book.Title.Contains("Code")).ToList();

        foreach (var book in bookWithCode)
        {
            Console.WriteLine($"Contains: {book.Title}");
        }

        var booksWithCodeInsensitive = context.Books.
            Where(book => EF.Functions.ILike(book.Title, "%code%")).ToList();

        foreach (var book in booksWithCodeInsensitive)
        {
            Console.WriteLine($"ILike: {book.Title}");
        }
    }
}