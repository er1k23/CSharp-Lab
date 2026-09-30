using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramFinanceBot.Commands;
using TelegramFinanceBot.Interfaces;

namespace TelegramFinanceBot.Services;

public class TelegramUpdateProcessor : ITelegramUpdateProcessor
{
    private readonly IEnumerable<ICommandHandler> _handlers;

    public TelegramUpdateProcessor(IEnumerable<ICommandHandler> handlers)
    {
        _handlers = handlers;
    }

    public async Task ProcessAsync(
        ITelegramBotClient botClient,
        Update update,
        CancellationToken cancellationToken)
    {
        if (update.Message is not { } message)
        {
            return;
        }

        if (message.Text is not { } messageText)
        {
            return;
        }
        
        var handlers = _handlers;

        ICommandHandler? handler;

        if (messageText.StartsWith("/"))
        {
            handler = handlers.FirstOrDefault(
                h => h.Command == messageText);

            if (handler is null)
            {
                await botClient.SendMessage(
                    chatId: message.Chat.Id,
                    text: "Unknown command. Use /help to see available commands.",
                    cancellationToken: cancellationToken);

                return;
            }
        }
        else
        {
            handler = handlers.FirstOrDefault(
                h => h.Command == CommandKeys.AddExpense);
        }

        if (handler is null)
        {
            return;
        }

        await handler.HandleAsync(
            botClient,
            message,
            cancellationToken);
    }
}