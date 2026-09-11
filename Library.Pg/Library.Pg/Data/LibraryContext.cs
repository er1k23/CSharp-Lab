using Microsoft.EntityFrameworkCore;
using Library.Pg.Models;

namespace Library.Pg.Data;

public class LibraryContext: DbContext
{
    public DbSet<Book> Books { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(
            "Host=localhost;Port=5432;Database=library;Username=postgres;Password=postgres");
    }
}