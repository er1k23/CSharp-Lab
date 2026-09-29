using TelegramFinanceBot.Models;

namespace TelegramFinanceBot.Interfaces;

public interface IExpenseService
{
    Task<Expense> CreateExpenseAsync(
        long chatId,
        decimal amount,
        string category,
        string? note);

    Task<List<Expense>> GetExpensesAsync(long userId);
}