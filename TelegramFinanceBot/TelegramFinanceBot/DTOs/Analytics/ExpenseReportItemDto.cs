namespace TelegramFinanceBot.DTOs.Analytics;

public class ExpenseReportItemDto
{
    public DateTime SpentAt { get; set; }
    
    public decimal Amount { get; set; }
    
    public string Category { get; set; } = string.Empty;
    
    public string? Note { get; set; }
}