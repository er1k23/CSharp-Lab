namespace TelegramFinanceBot.DTOs.Analytics;

public class DailyTotalDto
{
    public DateTime Date { get; set; }
    
    public decimal TotalAmount { get; set; }
    public int ExpenseCount { get; set; }
}