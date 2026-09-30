using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramFinanceBot.Interfaces;

namespace TelegramFinanceBot.Commands;

public class MonthCommandHandler : ICommandHandler
{
    private readonly IExpenseAnalyticsService _analyticsService;

    public MonthCommandHandler(IExpenseAnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }
    
    public string Command => CommandKeys.Month;

    public async Task HandleAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var chatId = message.Chat.Id;

        var nowUtc = DateTime.UtcNow;

        var summary = await _analyticsService.GetMonthlySummaryAsync(chatId,
            nowUtc);

        if (summary.ExpenseCount == 0)
        {
            await botClient.SendMessage(
                chatId: chatId,
                text: "You haven't added any expenses this month.",
                cancellationToken: cancellationToken);

            return;
        }
        
        var text =
            $"📊 This month's summary\n\n" +
            $"Total: {summary.MonthTotal:F2} AMD\n" +
            $"Expenses: {summary.ExpenseCount}\n" +
            $"Last 7 days: {summary.Last7DaysTotal:F2} AMD\n" +
            $"Previous 7 days: {summary.Previous7DaysTotal:F2} AMD\n" +
            $"Typical day: {summary.TypicalDayAmount:F2} AMD\n\n" +
            $"Top categories:\n";

        foreach (var category in summary.TopCategories)
        {
            text +=
                $"{category.Category}: "+
                $"{category.TotalAmount:F2} AMD" +
                $"({category.ExpenseCount} expenses)\n";
        }

        await botClient.SendMessage(
            chatId: chatId,
            text: text,
            cancellationToken:cancellationToken);
    }
}