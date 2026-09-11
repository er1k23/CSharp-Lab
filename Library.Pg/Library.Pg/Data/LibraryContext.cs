using Microsoft.EntityFrameworkCore;
using Library.Pg.Models;

namespace Library.Pg.Data;

public class LibraryContext: DbContext
{
    public DbSet<Book> Books { get; set; }
}