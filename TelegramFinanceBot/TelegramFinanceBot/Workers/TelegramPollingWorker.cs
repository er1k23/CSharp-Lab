using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TelegramFinanceBot.Models;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
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

        var chatId = message.Chat.Id;

        if (messageText == "/start")
        {
            await botClient.SendMessage(
                chatId: chatId,
                text: "Welcome 🤗 Your finance bot is running.",
                cancellationToken: cancellationToken);

            _logger.LogInformation(
                "Responded to /start in chat 😎 {ChatId}",
                chatId);
        }

        if (messageText == "/expenses")
        {
            using var scope = _scopeFactory.CreateScope();

            var expenseService = scope.ServiceProvider
                .GetRequiredService<IExpenseService>();

            var expenses = expenseService.GetExpenses();

            await botClient.SendMessage(
                chatId: chatId,
                text: $"You have {expenses.Count} expenses.",
                cancellationToken: cancellationToken);

            return;
        }
        
        else
        {
            string[] parts = messageText.Split(' ');

            if (parts.Length < 3)
            {
                await botClient.SendMessage(
                    chatId: chatId,
                    text: "Please use format: 1500 AMD food",
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

            using var scope = _scopeFactory.CreateScope();

            var expenseService = scope.ServiceProvider
                .GetRequiredService<IExpenseService>();

            var expense = expenseService.CreateExpense(
                amount,
                parts[1],
                category);
            
            await botClient.SendMessage(
                chatId: chatId,
                text: $"Amount: {expense.Amount} {expense.Currency}, Category: {expense.Category}",
                cancellationToken: cancellationToken);
        }
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