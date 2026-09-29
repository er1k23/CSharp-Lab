using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using TelegramFinanceBot.Configuration;
using TelegramFinanceBot.Data;
using TelegramFinanceBot.Interfaces;
using TelegramFinanceBot.Services;
using TelegramFinanceBot.Workers;
using TelegramFinanceBot.Commands;

// Create the application builder and load configuration.
var builder = WebApplication.CreateBuilder(args);

// Load Telegram settings from the "Telegram" configuration section.
builder.Services.Configure<TelegramOptions>(
    builder.Configuration.GetSection(TelegramOptions.SectionName));

// Register PostgreSQL database and Entity Framework Core.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Register one shared Telegram Bot client instance.
builder.Services.AddSingleton<ITelegramBotClient>(serviceProvider =>
{
    var telegramOptions = serviceProvider
        .GetRequiredService<IOptions<TelegramOptions>>()
        .Value;

    return new TelegramBotClient(telegramOptions.BotToken);
});

// Register the expense service with a scoped lifetime.
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<IChatService, ChatService>();


// Register command handlers.
builder.Services.AddScoped<ICommandHandler, StartCommandHandler>();
builder.Services.AddScoped<ICommandHandler, ExpensesCommandHandler>();
builder.Services.AddScoped<ICommandHandler, AddExpenseCommandHandler>();

// Register the background worker that receives Telegram updates.
builder.Services.AddHostedService<TelegramPollingWorker>();

// Build and run the application.
var app = builder.Build();

app.Run();