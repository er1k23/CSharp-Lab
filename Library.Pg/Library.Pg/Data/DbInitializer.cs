using Library.Pg.Models;

namespace Library.Pg.Data;

public static class DbInitializer
{
    public static void Seed(LibraryContext context)
    {
        if (context.Books.Any())
        {
            Console.WriteLine("Books already exist. Skipping seed.");
            return;
        }

        var books = new List<Book>
        {
            new()
            {
                Title = "Clean Code",
                Author = "Robert C. Martin",
                Price = 35.99m,
                PublishedOn = new DateTime(2008, 8, 1, 0, 0, 0, DateTimeKind.Utc),
                IsRead = true
            },
            new()
            {
                Title = "The Pragmatic Programmer",
                Author = "Andrew Hunt",
                Price = 42.99m,
                PublishedOn = new DateTime(1999, 10, 20, 0, 0, 0, DateTimeKind.Utc),
                IsRead = true
            },
            new()
            {
                Title = "Designing Data-Intensive Applications",
                Author = "Martin Kleppmann",
                Price = 49.99m,
                PublishedOn = new DateTime(2017, 3, 16, 0, 0, 0, DateTimeKind.Utc),
                IsRead = false
            },
            new()
            {
                Title = "Refactoring",
                Author = "Martin Fowler",
                Price = 47.99m,
                PublishedOn = new DateTime(1999, 7, 8, 0, 0, 0, DateTimeKind.Utc),
                IsRead = true
            },
            new()
            {
                Title = "Code Complete",
                Author = "Steve McConnell",
                Price = 39.99m,
                PublishedOn = new DateTime(2004, 6, 9, 0, 0, 0, DateTimeKind.Utc),
                IsRead = false
            },
            new()
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
    }
}