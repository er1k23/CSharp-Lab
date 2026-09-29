using TelegramFinanceBot.Models;
using TelegramFinanceBot.DTOs.Analytics;

namespace TelegramFinanceBot.Interfaces;

public interface IExpenseAnalyticsService
{
    Task<List<Expense>> GetExpensesForPeriodAsync(long chatId, DateTime fromUtc, DateTime toUtc);
    Task<MonthlySummaryDto> GetMonthlySummaryAsync(long chatId, DateTime nowUtc);
    Task<DetailedExpenseReportDto> GetDetailedReportAsync(long chatId, DateTime nowUtc);
}