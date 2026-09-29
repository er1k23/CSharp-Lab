using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramFinanceBot.Interfaces;

namespace TelegramFinanceBot.Commands;

public class ExpensesCommandHandler : ICommandHandler
{
    private readonly IExpenseService _expenseService;

    public ExpensesCommandHandler(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    public string Command => CommandKeys.Expenses;

    public async Task HandleAsync(
        ITelegramBotClient botClient,
        Message message,
        CancellationToken cancellationToken)
    {
        var chatId = message.Chat.Id;

        var expenses = await _expenseService.GetExpensesAsync(chatId);

        if (expenses.Count == 0)
        {
            await botClient.SendMessage(
                chatId: chatId,
                text: "You haven't added any expenses yet.",
                cancellationToken: cancellationToken);

            return;
        }

        var text = string.Join("\n\n", expenses.Select(expense =>
        {
            var result =
                $"{expense.Amount:F2} AMD - {expense.Category}";

            if (!string.IsNullOrWhiteSpace(expense.Note))
            {
                result += $"\nNote: {expense.Note}";
            }

            result += $"\nDate: {expense.SpentAt:yyyy-MM-dd HH:mm} UTC";

            return result;
        }));

        await botClient.SendMessage(
            chatId: chatId,
            text: text,
            cancellationToken: cancellationToken);
    }
}