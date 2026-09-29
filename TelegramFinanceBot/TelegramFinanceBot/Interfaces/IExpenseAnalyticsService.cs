using TelegramFinanceBot.Models;

namespace TelegramFinanceBot.Interfaces;

public interface IExpenseAnalyticsService
{
    Task<List<Expense>> GetExpensesForPeriodAsync(long chatId, DateTime fromUtc, DateTime toUtc);
}