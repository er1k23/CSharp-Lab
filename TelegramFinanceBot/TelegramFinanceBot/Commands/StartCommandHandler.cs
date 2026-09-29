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

        var chat = await _chatService.RegisterChatAsync(chatId);
        
        await botClient.SendMessage(
            chatId: message.Chat.Id,
            text: "Welcome 🤗 Your finance bot is running.",
            cancellationToken: cancellationToken);
    }
}