using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramFinanceBot.Commands;
using TelegramFinanceBot.Interfaces;

namespace TelegramFinanceBot.Services;

public class TelegramUpdateProcessor : ITelegramUpdateProcessor
{
    private readonly IEnumerable<ICommandHandler> _handlers;

    private readonly IChatService _chatService;

    public TelegramUpdateProcessor(IEnumerable<ICommandHandler> handlers, IChatService chatService)
    {
        _handlers = handlers;
        _chatService = chatService;
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

        var chatId = message.Chat.Id;

        if (messageText != CommandKeys.Start &&
            messageText != CommandKeys.Help)
        {
            var chatExists = await _chatService.ChatExistsAsync(chatId);

            if (!chatExists)
            {
                await botClient.SendMessage(
                    chatId: chatId,
                    text: "Please use /start first to start the finance assistant.",
                    cancellationToken: cancellationToken);

                return;
            }
            
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