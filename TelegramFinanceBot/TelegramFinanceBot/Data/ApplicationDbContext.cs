using Microsoft.EntityFrameworkCore;
using TelegramFinanceBot.Models;

namespace TelegramFinanceBot.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Expense> Expenses { get; set; }
}