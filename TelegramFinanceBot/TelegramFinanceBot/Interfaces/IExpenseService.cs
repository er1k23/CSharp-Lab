using TelegramFinanceBot.Models;

namespace TelegramFinanceBot.Interfaces;

public interface IExpenseService
{
    Expense CreateExpense(int amount, string currency, string category);

    List<Expense> GetExpenses();
}