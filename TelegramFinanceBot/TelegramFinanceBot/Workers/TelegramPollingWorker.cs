using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramFinanceBot.Workers;

public class TelegramPollingWorker : BackgroundService
{
    private readonly ITelegramBotClient _botClient;
    private readonly ILogger<TelegramPollingWorker> _logger;

    public TelegramPollingWorker(
        ITelegramBotClient botClient,
        ILogger<TelegramPollingWorker> logger)
    {
        _botClient = botClient;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>()
        };

        _botClient.StartReceiving(
            updateHandler: HandleUpdateAsync,
            errorHandler: HandlePollingErrorAsync,
            receiverOptions: receiverOptions,
            cancellationToken: stoppingToken);

        _logger.LogInformation("Telegram polling started.");

        try
        {
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Telegram polling is stopping.");
        }
    }

    private async Task HandleUpdateAsync(
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

        if (messageText == "/start")
        {
            await botClient.SendMessage(
                chatId: chatId,
                text: "Welcome 🤗 Your finance bot is running.",
                cancellationToken: cancellationToken);

            _logger.LogInformation(
                "Responded to /start in chat 😎 {ChatId}",
                chatId);
        }
        else
        {
            await botClient.SendMessage(
                chatId: chatId,
                text: "Hello 👋 Send /start to start using your finance bot.",
                cancellationToken: cancellationToken);
        }
    }

    private Task HandlePollingErrorAsync(
        ITelegramBotClient botClient,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(
            exception,
            "Telegram polling error.");

        return Task.CompletedTask;
    }
}