using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Library.Pg.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Library.Pg.Data;

public class LibraryContext: DbContext
{
    public DbSet<Book> Books { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(
            "Host=localhost;Port=5432;Database=library;Username=postgres;Password=postgres");

        optionsBuilder.LogTo(
            Console.WriteLine,
            new[] { DbLoggerCategory.Database.Command.Name },
            LogLevel.Information,
            DbContextLoggerOptions.SingleLine);

        optionsBuilder.EnableSensitiveDataLogging();
    }
}