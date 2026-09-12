using Library.Pg.Data;
using Library.Pg.Models;

namespace Library.Pg;

class Program
{
    static void Main(string[] args)
    {

        using var context = new LibraryContext();

        var canConnect = context.Database.CanConnect();

        Console.WriteLine($"Database connection: {canConnect}");

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        
        Console.WriteLine("Database schema created.");
        
        var books = new List<Book>
        {
            new Book
            {
                Title = "Clean Code",
                Author = "Robert C. Martin",
                Price = 35.99m,
                PublishedOn = new DateTime(2008, 8, 1, 0, 0, 0, DateTimeKind.Utc),
                IsRead = true
            },

            new Book
            {
                Title = "The Pragmatic Programmer",
                Author = "Andrew Hunt",
                Price = 42.99m,
                PublishedOn = new DateTime(1999, 10, 20, 0, 0, 0, DateTimeKind.Utc),
                IsRead = true
            },

            new Book
            {
                Title = "Designing Data-Intensive Applications",
                Author = "Martin Kleppmann",
                Price = 49.99m,
                PublishedOn = new DateTime(2017, 3, 16, 0, 0, 0, DateTimeKind.Utc),
                IsRead = false
            },

            new Book
            {
                Title = "Refactoring",
                Author = "Martin Fowler",
                Price = 47.99m,
                PublishedOn = new DateTime(1999, 7, 8, 0, 0, 0, DateTimeKind.Utc),
                IsRead = true
            },

            new Book
            {
                Title = "Code Complete",
                Author = "Steve McConnell",
                Price = 39.99m,
                PublishedOn = new DateTime(2004, 6, 9, 0, 0, 0, DateTimeKind.Utc),
                IsRead = false
            },

            new Book
            {
                Title = "SQL Antipatterns",
                Author = "Bill Karwin",
                Price = 34.99m,
                PublishedOn = new DateTime(2010, 2, 18, 0, 0, 0, DateTimeKind.Utc),
                IsRead = false
            }
        };
        
        context.Books.AddRange(books);
        context.SaveChanges();
        
        Console.WriteLine("Books seeded successfully.");


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
        
        
        
    }
}