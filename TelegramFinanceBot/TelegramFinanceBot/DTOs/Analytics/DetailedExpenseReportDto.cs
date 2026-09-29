namespace TelegramFinanceBot.DTOs.Analytics;

public class DetailedExpenseReportDto
{
    public MonthlySummaryDto Summary { get; set; } = new();

    public List<CategoryTotalDto> CategoryBreakdown { get; set; } = new();

    public List<DailyTotalDto> Last14Days { get; set; } = new();

    public List<ExpenseReportItemDto> RecentExpenses { get; set; } = new();
}