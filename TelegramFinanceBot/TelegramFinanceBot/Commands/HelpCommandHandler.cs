using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramFinanceBot.Interfaces;

namespace TelegramFinanceBot.Commands;

public class HelpCommandHandler : ICommandHandler
{
    public string Command => CommandKeys.Help;

    public async Task HandleAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var chatId = message.Chat.Id;

        const string text =
            "💰 Finance Assistant\n\n" +
            "Commands:\n" +
            "/start — Start the finance assistant\n" +
            "/today — View today's expenses\n" +
            "/month — View this month's summary\n" +
            "/expenses — View all your expenses\n" +
            "/help — Show available commands\n\n" +
            "📝 Add an expense:\n" +
            "<amount> <category> [note]\n\n" +
            "Examples:\n" +
            "4.50 coffee\n" +
            "12,50 food lunch\n" +
            "25 transport taxi\n\n" +
            "Amount must be positive. Category must contain letters only.";
        
        await botClient.SendMessage(
            chatId: chatId,
            text: text,
            cancellationToken: cancellationToken);
    }
}