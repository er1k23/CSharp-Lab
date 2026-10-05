using TelegramFinanceBot.Models;

namespace TelegramFinanceBot.Interfaces;

public interface IChatService
{
    Task<Chat> RegisterChatAsync(long chatId);
    
    Task<bool> ChatExistsAsync(long chatId);

    Task<List<Chat>> GetAllChatsAsync();
    
    Task<Chat?> GetChatByReportTokenAsync(string reportToken);
}