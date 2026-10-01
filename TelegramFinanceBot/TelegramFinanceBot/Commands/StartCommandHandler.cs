using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramFinanceBot.Interfaces;

namespace TelegramFinanceBot.Commands;

public class StartCommandHandler : ICommandHandler
{
    private readonly IChatService _chatService;

    public StartCommandHandler(IChatService chatService)
    {
        _chatService = chatService;
    }
    
    
    public string Command => CommandKeys.Start;

    public async Task HandleAsync(
        ITelegramBotClient botClient,
        Message message,
        CancellationToken cancellationToken)
    {
        var chatId = message.Chat.Id;

        await _chatService.RegisterChatAsync(chatId);
        
        var text =
            "💰 Finance Assistant\n\n" +
            "Welcome! Your chat has been registered.\n\n" +
            "📝 Add an expense:\n" +
            "<amount> <category> [note]\n\n" +
            "Examples:\n" +
            "4.50 coffee\n" +
            "12,50 food lunch\n" +
            "25 transport taxi\n\n" +
            "Commands:\n" +
            "/today — View today's expenses\n" +
            "/month — View this month's summary\n" +
            "/expenses — View all your expenses\n" +
            "/help — Show available commands";
        
        await botClient.SendMessage(
            chatId: message.Chat.Id,
            text: text,
            cancellationToken: cancellationToken);
    }
}