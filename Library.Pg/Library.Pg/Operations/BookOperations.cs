using Library.Pg.Data;

namespace Library.Pg.Operations;

public class BookOperations
{
    public static void RunAll(LibraryContext context)
    {
        
        // ====================
        // 5.1 — Update
        // ====================
        
        var book = context.Books.Find(1);
        
        if (book != null)
        {
            book.IsRead = false;
            
            context.SaveChanges();
            
            Console.WriteLine($"Updated: {book.Title}, IsRead = {book.IsRead}");
        }
        
        // ====================
        // 5.2 — Delete
        // ====================
        
        var bookToDelete = context.Books.Find(6);

        if (bookToDelete != null)
        {
            context.Books.Remove(bookToDelete);
            context.SaveChanges();
            
            Console.WriteLine(
                $"Deleted: {bookToDelete.Title}"
            );
        }

        var existingBook = context.Books.Find(1);

        if (existingBook != null)
        {
            context.Books.Add(existingBook);

            try
            {
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Expected error: {ex.Message}");
            }
        }

    }
}