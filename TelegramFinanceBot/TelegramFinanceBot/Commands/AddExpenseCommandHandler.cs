using System.Globalization;
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

        string[] parts = messageText.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length < 2)
        {
            await botClient.SendMessage(
                chatId: chatId,
                text: "Invalid format. Use: <amount> <category> [note]\nExample: 3310 groceries lidl milk",
                cancellationToken: cancellationToken);

            return;
        }

        string amountText = parts[0].Replace(',', '.');
        
        if (!decimal.TryParse(
                amountText,
                NumberStyles.AllowDecimalPoint,
                CultureInfo.InvariantCulture,
                out decimal amount))
        {
            await botClient.SendMessage(
                chatId: chatId,
                text: "Invalid amount. Please enter a number, e.g. 12.50.",
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

        string category = parts[1];

        if (!category.All(char.IsLetter))
        {
            await botClient.SendMessage(
                chatId: chatId,
                text: "Category must contain letters only.",
                cancellationToken: cancellationToken);
            
            return;
        }

        category = category.ToLowerInvariant();

        string? note = parts.Length > 2 ? string.Join(' ', parts.Skip(2)) : null;

        var expense = await _expenseService.CreateExpenseAsync(
            chatId,
            amount,
            category,
            note);
        
        string response = $"Saved: {expense.Amount:F2} AMD, Category: {expense.Category}";

        if (expense.Note is not null)
        {
            response += $", Note: {expense.Note}";
        }
        
        await botClient.SendMessage(
            chatId: chatId,
            text: response,
            cancellationToken: cancellationToken);
    }
}