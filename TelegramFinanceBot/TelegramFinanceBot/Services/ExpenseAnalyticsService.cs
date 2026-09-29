using Microsoft.EntityFrameworkCore;
using TelegramFinanceBot.Data;
using TelegramFinanceBot.DTOs.Analytics;
using TelegramFinanceBot.Models;
using TelegramFinanceBot.Interfaces;
using System.Linq;

namespace TelegramFinanceBot.Services;

public class ExpenseAnalyticsService : IExpenseAnalyticsService
{
    private readonly ApplicationDbContext _context;

    public ExpenseAnalyticsService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Expense>> GetExpensesForPeriodAsync(long chatId, DateTime fromUtc, DateTime toUtc)
    {
        return await _context.Expenses.Where
            (e => e.ChatId == chatId && e.SpentAt >= fromUtc && e.SpentAt < toUtc)
            .ToListAsync();
    }

    public async Task<MonthlySummaryDto> GetMonthlySummaryAsync(long chatId, DateTime nowUtc)
    {
        var monthStartUtc = new DateTime(
            nowUtc.Year,
            nowUtc.Month,
            1,
            0, 0, 0,
            DateTimeKind.Utc);

        var todayStartUtc =
            nowUtc.Date;

        var monthExpenses =
            await GetExpensesForPeriodAsync(chatId, monthStartUtc, nowUtc);

        var last7DaysExpenses =
            await GetExpensesForPeriodAsync(chatId, todayStartUtc.AddDays(-7), todayStartUtc);

        var previous7DaysExpenses =
            await GetExpensesForPeriodAsync(chatId, todayStartUtc.AddDays(-14), todayStartUtc.AddDays(-7));

        decimal monthTotal =
            monthExpenses.Sum(e => e.Amount);

        var topCategories = monthExpenses
            .GroupBy(e => e.Category)
            .Select(group => new CategoryTotalDto
            {
                Category = group.Key,
                TotalAmount = group.Sum(e => e.Amount),
                ExpenseCount = group.Count()
            }).OrderByDescending(category => category.TotalAmount).Take(3).ToList();

        return new MonthlySummaryDto
        {
            MonthTotal = monthTotal,
            ExpenseCount = monthExpenses.Count,
            Last7DaysTotal = last7DaysExpenses.Sum(e => e.Amount),
            Previous7DaysTotal = previous7DaysExpenses.Sum(e => e.Amount),
            TypicalDayAmount = monthTotal / nowUtc.Day,
            TopCategories = topCategories

        };
    }

    public async Task<DetailedExpenseReportDto> GetDetailedReportAsync(long chatId, DateTime nowUtc)
    {
        // 1. Get monthly summary
        var summary = await GetMonthlySummaryAsync(chatId, nowUtc);

        // 2. Get expenses for the current month
        var monthStartUtc = new DateTime(
            nowUtc.Year,
            nowUtc.Month,
            1,
            0, 0, 0,
            DateTimeKind.Utc);

        var monthExpenses = await GetExpensesForPeriodAsync(
            chatId,
            monthStartUtc,
            nowUtc);

        // 3. Full category breakdown
        var categoryBreakdown = monthExpenses
            .GroupBy(e => e.Category)
            .Select(group => new CategoryTotalDto
            {
                Category = group.Key,
                TotalAmount = group.Sum(e => e.Amount),
                ExpenseCount = group.Count()
            })
            .OrderByDescending(category => category.TotalAmount)
            .ToList();

        // 4. Last 14 calendar days
        var todayStartUtc = nowUtc.Date;
        var last14DaysStartUtc = todayStartUtc.AddDays(-13);
        var tomorrowStartUtc = todayStartUtc.AddDays(1);

        var last14DaysExpenses = await GetExpensesForPeriodAsync(
            chatId,
            last14DaysStartUtc,
            tomorrowStartUtc);

        // 5. Group expenses by day
        var dailyGroups = last14DaysExpenses
            .GroupBy(e => e.SpentAt.Date)
            .ToDictionary(
                group => group.Key,
                group => new DailyTotalDto
                {
                    Date = group.Key,
                    TotalAmount = group.Sum(e => e.Amount),
                    ExpenseCount = group.Count()
                });

        // 6. Include days with zero expenses
        var dailyTotals = Enumerable
            .Range(0, 14)
            .Select(offset =>
            {
                var date = last14DaysStartUtc.AddDays(offset);

                if (dailyGroups.TryGetValue(date, out var dailyTotal))
                    return dailyTotal;

                return new DailyTotalDto
                {
                    Date = date,
                    TotalAmount = 0,
                    ExpenseCount = 0
                };
            })
            .ToList();

        // 7. Get the latest 20 expenses
        var recentExpenses = await _context.Expenses
            .Where(e => e.ChatId == chatId)
            .OrderByDescending(e => e.SpentAt)
            .ThenByDescending(e => e.Id)
            .Take(20)
            .Select(e => new ExpenseReportItemDto
            {
                SpentAt = e.SpentAt,
                Amount = e.Amount,
                Category = e.Category,
                Note = e.Note
            })
            .ToListAsync();

        // 8. Return the complete report
        return new DetailedExpenseReportDto
        {
            Summary = summary,
            CategoryBreakdown = categoryBreakdown,
            Last14Days = dailyTotals,
            RecentExpenses = recentExpenses
        };
    }
}