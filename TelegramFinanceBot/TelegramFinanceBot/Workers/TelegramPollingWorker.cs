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
        using var scope = _scopeFactory.CreateScope();

        var processor = scope.ServiceProvider
            .GetRequiredService<ITelegramUpdateProcessor>();

        await processor.ProcessAsync(
            botClient,
            update,
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