using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using TelegramFinanceBot.Configuration;
using TelegramFinanceBot.Data;
using TelegramFinanceBot.Interfaces;
using TelegramFinanceBot.Services;
using TelegramFinanceBot.Workers;


var builder = Host.CreateApplicationBuilder(args);

// Telegram configuration
builder.Services.Configure<TelegramOptions>(
    builder.Configuration.GetSection(TelegramOptions.SectionName));

// PostgreSQL and EF Core
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Telegram Bot
builder.Services.AddSingleton<ITelegramBotClient>(serviceProvider =>
{
    var telegramOptions = serviceProvider
        .GetRequiredService<IOptions<TelegramOptions>>()
        .Value;

    return new TelegramBotClient(telegramOptions.BotToken);
});

// Expense service
builder.Services.AddScoped<IExpenseService, ExpenseService>();

// Background worker
builder.Services.AddHostedService<TelegramPollingWorker>();

var app = builder.Build();


await app.RunAsync();