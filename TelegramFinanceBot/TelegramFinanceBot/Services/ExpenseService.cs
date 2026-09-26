using TelegramFinanceBot.Interfaces;
using TelegramFinanceBot.Models;

namespace TelegramFinanceBot.Services;

public class ExpenseService : IExpenseService
{
    public Expense CreateExpense(int amount, string currency, string category)
    {
        var expense = new Expense
        {
            Amount = amount,
            Currency = currency,
            Category = category
        };
        return expense;
    }
}