using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TelegramFinanceBot.Interfaces;
using Microsoft.Extensions.Options;
using TelegramFinanceBot.Configuration;
using TelegramFinanceBot.DTOs.Report;

namespace TelegramFinanceBot.Controllers;

[AllowAnonymous]
[Route("report")]
public class ReportController : Controller
{
    private readonly IChatService _chatService;
    private readonly IExpenseAnalyticsService _analyticsService;
    private readonly IOptions<TelegramOptions> _telegramOptions;
    
    public ReportController(
        IChatService chatService,
        IExpenseAnalyticsService analyticsService,
        IOptions<TelegramOptions> telegramOptions)
    {
        _analyticsService = analyticsService;
        _chatService = chatService;
        _telegramOptions = telegramOptions;
    }

    [HttpGet("{token}")]
    public async Task<IActionResult> Index(string token)
    {
        var chat = await _chatService.GetChatByReportTokenAsync(token);

        if (chat == null)
        {
            return NotFound();
        }

        var report = await _analyticsService.GetDetailedReportAsync(
            chat.Id,
            DateTime.UtcNow);

        return View(new ReportViewModel
        {
            Report = report,
            Currency = _telegramOptions.Value.Currency
        });
    }
}