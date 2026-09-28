using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TelegramFinanceBot.Models;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramFinanceBot.Interfaces;
using TelegramFinanceBot.Commands;
using TelegramFinanceBot.Interfaces;

namespace TelegramFinanceBot.Workers;

public class TelegramPollingWorker : BackgroundService
{
    private readonly ITelegramBotClient _botClient;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<TelegramPollingWorker> _logger;

    public TelegramPollingWorker(
        ITelegramBotClient botClient,
        ILogger<TelegramPollingWorker> logger,
        IServiceScopeFactory scopeFactory)
    {
        _botClient = botClient;
        _logger = logger;
        _scopeFactory = scopeFactory;
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

        using var scope = _scopeFactory.CreateScope();

        var handlers = scope.ServiceProvider
            .GetServices<ICommandHandler>();

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