using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramFinanceBot.Interfaces;

public interface ICommandHandler
{
    string Command { get; }

    Task HandleAsync(
        ITelegramBotClient botClient,
        Message message,
        CancellationToken cancellationToken);
}