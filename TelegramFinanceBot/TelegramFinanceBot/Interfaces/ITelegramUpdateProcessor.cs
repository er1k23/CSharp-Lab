using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramFinanceBot.Interfaces;

public interface ITelegramUpdateProcessor
{
    Task ProcessAsync(
        ITelegramBotClient botClient,
        Update update,
        CancellationToken cancellationToken);
}