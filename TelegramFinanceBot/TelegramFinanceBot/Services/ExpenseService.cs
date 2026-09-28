using Microsoft.EntityFrameworkCore;
using TelegramFinanceBot.Data;
using TelegramFinanceBot.Interfaces;
using TelegramFinanceBot.Models;

namespace TelegramFinanceBot.Services;

public class ExpenseService : IExpenseService
{
    private readonly ApplicationDbContext _context;

    public ExpenseService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Expense> CreateExpenseAsync(
        long userId,
        int amount,
        string currency,
        string category)
    {
        var expense = new Expense
        {
            UserId = userId,
            Amount = amount,
            Currency = currency,
            Category = category
        };

        _context.Expenses.Add(expense);

        await _context.SaveChangesAsync();

        return expense;
    }

    public async Task<List<Expense>> GetExpensesAsync(long userid)
    {
        return await _context.Expenses.Where(u => u.UserId == userid).ToListAsync();
    }
}