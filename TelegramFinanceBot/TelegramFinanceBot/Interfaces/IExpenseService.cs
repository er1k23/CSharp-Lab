using TelegramFinanceBot.Models;

namespace TelegramFinanceBot.Interfaces;

public interface IExpenseService
{
    Task<Expense> CreateExpenseAsync(
        int amount,
        string currency,
        string category);

    Task<List<Expense>> GetExpensesAsync();
}