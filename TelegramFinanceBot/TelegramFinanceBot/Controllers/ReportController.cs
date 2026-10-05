using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TelegramFinanceBot.Interfaces;

namespace TelegramFinanceBot.Controllers;

[AllowAnonymous]
[Route("report")]
public class ReportController : Controller
{
    private readonly IChatService _chatService;
    private readonly IExpenseAnalyticsService _analyticsService;

    public ReportController(
        IChatService chatService,
        IExpenseAnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
        _chatService = chatService;
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

        return View(report);
    }
}