using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramFinanceBot.Interfaces;
using TelegramFinanceBot.Configuration;

namespace TelegramFinanceBot.Workers;

public class DailyDigestWorker : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<DailyDigestWorker> _logger;
    private readonly TelegramOptions _telegramOptions;
    private readonly ITelegramBotClient _botClient;

    public DailyDigestWorker(
        IServiceScopeFactory scopeFactory,
        ILogger<DailyDigestWorker> logger,
        IOptions<TelegramOptions> telegramOptions,
        ITelegramBotClient botClient)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
        _telegramOptions = telegramOptions.Value;
        _botClient = botClient;
    }

    private DateTime GetNextRunUtc()
    {
        var nowUtc = DateTime.UtcNow;
        var nextRun = nowUtc.Date.AddHours(_telegramOptions.DigestHourUtc);
        
        if (nextRun <= nowUtc)
        {
            nextRun = nextRun.AddDays(1);
        }

        return nextRun;
    }
    
    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        _logger.LogInformation("Daily digest worker started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            var nextRunUtc = GetNextRunUtc();
            
            var delay = nextRunUtc - DateTime.UtcNow;
            
            _logger.LogInformation("Next daily digest scheduled for {NextRunUtc}.",
                nextRunUtc);
            
            await Task.Delay(delay, stoppingToken);
            
            using var scope = _scopeFactory.CreateScope();

            var chatService = scope.ServiceProvider.GetRequiredService<IChatService>();

            var analyticsService = scope.ServiceProvider.GetRequiredService<IExpenseAnalyticsService>();

            var chats = await chatService.GetAllChatsAsync();

            foreach (var chat in chats)
            {
                var summary = await analyticsService.GetMonthlySummaryAsync(chat.Id, DateTime.UtcNow);
                
                if (summary.ExpenseCount == 0)
                {
                    continue;
                }

                var reportUrl = $"{_telegramOptions.PublicBaseUrl}/report/{chat.ReportToken}";
                
                var message =
                    $"💰 This month — {summary.MonthTotal} {_telegramOptions.Currency} " +
                    $"( {summary.ExpenseCount} entries)\n\n" +
                    $"📅 Last 7 days — {summary.Last7DaysTotal} {_telegramOptions.Currency}\n" +
                    $"📅 Previous 7 days — {summary.Previous7DaysTotal} {_telegramOptions.Currency}\n" +
                    $"📊 Typical day — {summary.TypicalDayAmount:F2} {_telegramOptions.Currency}\n\n" +
                    "🏷 Top categories:\n";

                foreach (var category in summary.TopCategories)
                {
                    message += $"• {category.Category} — {category.TotalAmount} {_telegramOptions.Currency}\n";
                }
                
                message +=
                    $"\n🔗 Detailed report:\n{reportUrl}";

                await _botClient.SendMessage(
                    chatId: chat.Id,
                    text: message,
                    cancellationToken: stoppingToken);
                
                _logger.LogInformation(
                    "Daily digest sent to chat {ChatId}.",
                    chat.Id);
            }
        }
    }
}