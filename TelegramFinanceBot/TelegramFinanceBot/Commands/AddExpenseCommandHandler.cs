using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramFinanceBot.Interfaces;

namespace TelegramFinanceBot.Commands;

public class AddExpenseCommandHandler : ICommandHandler
{
    private readonly IExpenseService _expenseService;

    public AddExpenseCommandHandler(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    public string Command => CommandKeys.AddExpense;

    public async Task HandleAsync(
        ITelegramBotClient botClient,
        Message message,
        CancellationToken cancellationToken)
    {
        var messageText = message.Text!;
        var chatId = message.Chat.Id;
        var userId = message.From!.Id;

        string[] parts = messageText.Split(' ');

        if (parts.Length < 3)
        {
            await botClient.SendMessage(
                chatId: chatId,
                text: "Please use format: (Amount) AMD (Category)",
                cancellationToken: cancellationToken);

            return;
        }

        if (!int.TryParse(parts[0], out int amount))
        {
            await botClient.SendMessage(
                chatId: chatId,
                text: "Invalid amount. Please enter a number.",
                cancellationToken: cancellationToken);

            return;
        }

        if (amount <= 0)
        {
            await botClient.SendMessage(
                chatId: chatId,
                text: "Amount must be greater than zero.",
                cancellationToken: cancellationToken);

            return;
        }

        string category = parts[2];

        var expense = await _expenseService.CreateExpenseAsync(
            userId,
            amount,
            parts[1],
            category);

        await botClient.SendMessage(
            chatId: chatId,
            text: $"Amount: {expense.Amount} {expense.Currency}, Category: {expense.Category}",
            cancellationToken: cancellationToken);
    }
}