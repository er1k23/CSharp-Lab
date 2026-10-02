using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using TelegramFinanceBot.Configuration;

namespace TelegramFinanceBot.Workers;

public class WebhookSetupWorker : BackgroundService
{
    private readonly ITelegramBotClient _botClient;
    private readonly TelegramOptions _telegramOptions;

    public WebhookSetupWorker(
        ITelegramBotClient botClient,
        IOptions<TelegramOptions> telegramOptions)
    {
        _botClient = botClient;
        _telegramOptions = telegramOptions.Value;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        if (string.IsNullOrWhiteSpace(_telegramOptions.PublicBaseUrl))
        {
            return;
        }
        
        var webhookUrl =
            $"{_telegramOptions.PublicBaseUrl}/bot/{_telegramOptions.WebhookSecret}";

        await _botClient.SetWebhook(
            webhookUrl,
            cancellationToken: stoppingToken);
    }
}