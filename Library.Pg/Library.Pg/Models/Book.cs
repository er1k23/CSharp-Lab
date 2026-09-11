namespace Library.Pg.Models;

public class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;
    
    public decimal Price { get; set; }
    
    public DateTime PublishedOn { get; set; }
    
    public bool IsRead { get; set; }
}