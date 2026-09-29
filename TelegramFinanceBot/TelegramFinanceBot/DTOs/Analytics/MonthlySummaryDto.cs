namespace TelegramFinanceBot.DTOs.Analytics;

public class MonthlySummaryDto
{
    public decimal MonthTotal { get; set; }

    public int ExpenseCount { get; set; }

    public decimal Last7DaysTotal { get; set; }

    public decimal Previous7DaysTotal { get; set; }

    public decimal TypicalDayAmount { get; set; }

    public List<CategoryTotalDto> TopCategories { get; set; } = new();
}