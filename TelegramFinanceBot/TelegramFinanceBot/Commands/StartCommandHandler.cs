using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramFinanceBot.Interfaces;

namespace TelegramFinanceBot.Commands;

public class StartCommandHandler : ICommandHandler
{
    public string Command => CommandKeys.Start;

    public async Task HandleAsync(
        ITelegramBotClient botClient,
        Message message,
        CancellationToken cancellationToken)
    {
        await botClient.SendMessage(
            chatId: message.Chat.Id,
            text: "Welcome 🤗 Your finance bot is running.",
            cancellationToken: cancellationToken);
    }
}