namespace TelegramFinanceBot.Models;

public class Expense
{
    public int Amount { get; set; }

    public string Currency { get; set; } = "AMD";

    public string Category { get; set; } = "";
}