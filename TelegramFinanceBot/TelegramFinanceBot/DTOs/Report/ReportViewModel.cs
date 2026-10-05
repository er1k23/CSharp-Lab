using TelegramFinanceBot.DTOs.Analytics;

namespace TelegramFinanceBot.DTOs.Report;

public class ReportViewModel
{
    public DetailedExpenseReportDto Report { get; set; } = new();
    public string Currency { get; set; } = "";
}