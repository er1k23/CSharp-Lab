using TelegramFinanceBot.Interfaces;
using TelegramFinanceBot.Models;

namespace TelegramFinanceBot.Services;

public class ExpenseService : IExpenseService
{

    private readonly List<Expense> _expenses = new();
    
    public Expense CreateExpense(int amount, string currency, string category)
    {
        var expense = new Expense
        {
            Amount = amount,
            Currency = currency,
            Category = category
        };
        
        _expenses.Add(expense);
        
        return expense;
    }

    public List<Expense> GetExpenses()
    {
        return _expenses;
    }
}