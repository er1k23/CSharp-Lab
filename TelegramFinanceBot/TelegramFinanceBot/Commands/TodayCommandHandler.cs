using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramFinanceBot.Interfaces;
using TelegramFinanceBot.Services;

namespace TelegramFinanceBot.Commands;

public class TodayCommandHandler : ICommandHandler
{
    private readonly IExpenseAnalyticsService _analyticsService;

    public TodayCommandHandler(IExpenseAnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }
    
    public string Command => CommandKeys.Today;

    public async Task HandleAsync(
        ITelegramBotClient botClient,
        Message message,
        CancellationToken cancellationToken)
    {
        var chatId = message.Chat.Id;

        var nowUtc = DateTime.UtcNow;
        var todayStartUtc = nowUtc.Date;
        var tomorrowStartUtc = todayStartUtc.AddDays(1);

        var expenses = await _analyticsService.GetExpensesForPeriodAsync(chatId, todayStartUtc, tomorrowStartUtc);
        
            if (expenses.Count == 0)
            {
                await botClient.SendMessage(
                    chatId: chatId,
                    text: "You haven't added any expenses today.",
                    cancellationToken: cancellationToken);
                
                return;
            }

            var total = expenses.Sum(e => e.Amount);
            
            var text = $"Today's expenses: {total:F2} AMD\n\n";
            
            text += string.Join("\n", expenses.Select(e =>
                $"{e.Amount:F2} AMD - {e.Category}" +
                (string.IsNullOrWhiteSpace(e.Note) ? "" : $" ({e.Note})")));

            await botClient.SendMessage(
                chatId: chatId,
                text: text,
                cancellationToken: cancellationToken);
    }
}