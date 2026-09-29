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
        long chatId,
        decimal amount,
        string category,
        string? note)
    {
        var expense = new Expense
        {
            ChatId = chatId,
            Amount = amount,
            Category = category,
            Note = note,
            SpentAt = DateTime.UtcNow
        };

        _context.Expenses.Add(expense);

        await _context.SaveChangesAsync();

        return expense;
    }

    public async Task<List<Expense>> GetExpensesAsync(long chatId)
    {
        return await _context.Expenses
            .Where(e => e.ChatId == chatId)
            .ToListAsync();
    }
}