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
        var userId = message.From!.Id;

        var expenses = await _expenseService.GetExpensesAsync(userId);

        var text = string.Join("\n", expenses.Select(expense =>
            $"{expense.Amount} {expense.Currency} - {expense.Category}"));

        await botClient.SendMessage(
            chatId: message.Chat.Id,
            text: text,
            cancellationToken: cancellationToken);
    }
}