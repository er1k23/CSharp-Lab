using TelegramFinanceBot.Models;

namespace TelegramFinanceBot.Interfaces;

public interface IChatService
{
    Task<Chat> RegisterChatAsync(long chatId);
}