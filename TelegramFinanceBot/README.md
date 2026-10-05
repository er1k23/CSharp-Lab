# 💰 Telegram Finance Bot

A personal finance assistant built with **C# and ASP.NET Core** that allows users to record expenses through Telegram, receive spending summaries, and view a detailed financial dashboard in the browser.

The project was built as a backend-focused application to practice **ASP.NET Core, dependency injection, Entity Framework Core, PostgreSQL, Telegram Bot API, background services, MVC/Razor and clean application structure**.

---

## ✨ Features

### Telegram Bot

- `/start` — register the chat and initialize the finance assistant
- `/help` — show available commands
- `/today` — view today's expenses
- `/month` — view the current month's summary
- `/expenses` — view recorded expenses
- Add expenses directly through messages

### Expense Format

Expenses are entered using:

```text
<amount> <category> [note]
```

Examples:

```text
800 coffee
1500 groceries
450 transport taxi
700 food lunch with friends
```

The bot supports both decimal separators:

```text
12.50 coffee
12,50 coffee
```

Categories are stored in lowercase.

---

## 📊 Financial Analytics

The application calculates:

- Total spending for the current month
- Number of expenses
- Last 7 days total
- Previous 7 days total
- Typical daily spending
- Top 3 spending categories
- Full category breakdown
- Daily spending for the last 14 days
- Last 20 individual expenses

All date calculations use **UTC**.

---

## 📅 Daily Digest

A hosted background worker sends a daily financial summary to registered Telegram chats.

The digest contains:

- Current month total
- Number of expenses
- Last 7 days total
- Previous 7 days total
- Typical daily spending
- Top 3 categories
- Link to the detailed financial report

The worker calculates the next execution time based on the configured UTC hour instead of simply waiting 24 hours after application startup.

---

## 🌐 Financial Dashboard

Every registered chat receives a secure report link:

```text
/report/{token}
```

The report provides a full browser-based financial dashboard with:

- Monthly spending overview
- KPI cards
- Spending comparison
- Category breakdown
- Top categories
- 14-day spending activity
- Recent transactions
- Responsive layout for desktop and mobile

The dashboard is implemented using:

- ASP.NET Core MVC
- Razor
- HTML
- CSS

The report token is randomly generated and is used instead of exposing the Telegram chat ID in the URL.

---

## 🏗️ Architecture

The application follows a layered structure:

```text
Telegram
    │
    ▼
Webhook
    │
    ▼
TelegramUpdateProcessor
    │
    ▼
Services
    │
    ▼
Entity Framework Core
    │
    ▼
PostgreSQL
```

The report flow is:

```text
Browser
    │
    ▼
ReportController
    │
    ▼
ExpenseAnalyticsService
    │
    ▼
DetailedExpenseReportDto
    │
    ├── TelegramOptions.Currency
    │
    ▼
ReportViewModel
    │
    ▼
Razor View
    │
    ▼
HTML + CSS
```

---

## 🧰 Tech Stack

| Technology | Purpose |
|---|---|
| C# | Main programming language |
| .NET 10 | Application platform |
| ASP.NET Core | Web application and API infrastructure |
| Telegram.Bot | Telegram Bot API integration |
| Entity Framework Core | ORM / database access |
| PostgreSQL | Relational database |
| Npgsql | PostgreSQL provider for EF Core |
| Docker | PostgreSQL container |
| MVC / Razor | Web report rendering |
| HTML / CSS | Financial dashboard |
| Dependency Injection | Application service composition |
| BackgroundService | Daily digest |
| Webhook | Telegram update delivery |

---

## 📁 Project Structure

```text
TelegramFinanceBot/
│
├── Commands/
│   ├── AddExpenseCommandHandler.cs
│   ├── CommandKeys.cs
│   ├── ExpensesCommandHandler.cs
│   └── StartCommandHandler.cs
│
├── Configuration/
│   └── TelegramOptions.cs
│
├── Controllers/
│   ├── ReportController.cs
│   └── WebhookController.cs
│
├── Data/
│   └── ApplicationDbContext.cs
│
├── DTOs/
│   ├── Analytics/
│   └── Report/
│       └── ReportViewModel.cs
│
├── Interfaces/
│   ├── IChatService.cs
│   ├── IExpenseAnalyticsService.cs
│   ├── IExpenseService.cs
│   └── ITelegramUpdateProcessor.cs
│
├── Models/
│   ├── Chat.cs
│   └── Expense.cs
│
├── Services/
│   ├── ChatService.cs
│   ├── ExpenseAnalyticsService.cs
│   ├── ExpenseService.cs
│   └── TelegramUpdateProcessor.cs
│
├── Views/
│   └── Report/
│       └── Index.cshtml
│
├── Workers/
│   ├── DailyDigestWorker.cs
│   └── WebhookSetupWorker.cs
│
├── wwwroot/
│   └── css/
│       └── report.css
│
├── Program.cs
└── TelegramFinanceBot.csproj
```

---

## ⚙️ Configuration

Application configuration contains the following settings:

```json
{
  "Telegram": {
    "BotToken": "",
    "Currency": "AMD",
    "DigestHourUtc": 18
  },
  "PublicBaseUrl": ""
}
```

### Configuration

| Setting | Description |
|---|---|
| `BotToken` | Telegram Bot API token |
| `Currency` | Currency label displayed by the application |
| `DigestHourUtc` | UTC hour when the daily digest is sent |
| `PublicBaseUrl` | Public URL used to generate report links |

### Security

The Telegram bot token and other secrets should **never be committed to Git**.

For local development, sensitive values are stored using **.NET User Secrets**.

---

## 🐘 Database

PostgreSQL runs in Docker.

The application uses Entity Framework Core migrations to create and update the database schema.

Main entities:

### Chat

Stores:

- Telegram chat ID
- Secure report token
- Registration time

### Expense

Stores:

- Chat ID
- Amount
- Category
- Optional note
- UTC timestamp

Relationship:

```text
Chat
 │
 └──< Expense
```

One chat can have many expenses.

---

## 🚀 Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/er1k23/CSharp-Lab.git
cd CSharp-Lab/TelegramFinanceBot/TelegramFinanceBot
```

### 2. Configure secrets

Set the Telegram bot token and database connection string using .NET User Secrets.

Example:

```bash
dotnet user-secrets init
```

Then configure the required secrets.

### 3. Start PostgreSQL

Start the PostgreSQL Docker container used by the application.

### 4. Apply migrations

```bash
dotnet ef database update
```

### 5. Run the application

```bash
dotnet run
```

### 6. Open Telegram

Open the configured Telegram bot and run:

```text
/start
```

Then add an expense:

```text
500 coffee
```

---

## 🧪 Example

```text
User:
    700 coffee morning

Bot:
    Saved 700 AMD · coffee
```

Then:

```text
/month
```

returns a monthly summary.

The detailed report can be opened through:

```text
/report/{token}
```

---

## 🔐 Report Security

Report URLs use a randomly generated token instead of the Telegram chat ID.

A token is generated using a cryptographically secure random generator:

```csharp
Convert.ToHexString(
    RandomNumberGenerator.GetBytes(16)
);
```

An unknown token results in:

```text
404 Not Found
```

The Telegram chat ID is therefore not exposed in the report URL.

---

## 🎯 Project Goals

The project was developed to practice real-world ASP.NET Core concepts, including:

- Dependency Injection
- Service Layer
- Interfaces
- Entity Framework Core
- PostgreSQL
- REST / HTTP endpoints
- MVC
- Razor Views
- DTOs and ViewModels
- Background Services
- Webhooks
- Configuration
- Secure token generation
- Database migrations
- Responsive frontend development

---

## 📌 Future Improvements

Possible future extensions:

- Authentication for the web dashboard
- Expense editing and deletion
- Income tracking
- Budget management
- Charts and visual analytics
- Export to CSV / PDF
- Filtering and pagination
- Unit and integration tests
- Docker Compose for the complete application
- Production deployment

---

## 👨‍💻 Author

**Erik Rumian**

C# / .NET backend project focused on ASP.NET Core, EF Core, PostgreSQL and Telegram Bot API.