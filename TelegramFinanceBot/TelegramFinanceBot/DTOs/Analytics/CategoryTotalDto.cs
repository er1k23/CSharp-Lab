namespace TelegramFinanceBot.DTOs.Analytics;

public class CategoryTotalDto
{
    public string Category { get; set; } = "";
    
    public decimal TotalAmount { get; set; }
    
    public int ExpenseCount { get; set; }
}