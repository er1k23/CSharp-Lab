namespace TelegramFinanceBot.Models;

public class Chat
{
    public long Id { get; set; }
    
    public string ReportToken { get; set; } = "";
    
    public DateTime StartedAt { get; set; }
}