using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramFinanceBot.Interfaces;
using Microsoft.Extensions.Options;
using TelegramFinanceBot.Configuration;

namespace TelegramFinanceBot.Controllers;

[ApiController]
[Route("bot")]
public class WebhookController : ControllerBase
{
    private readonly ITelegramBotClient _botClient;
    private readonly ITelegramUpdateProcessor _updateProcessor;
    private readonly TelegramOptions _telegramOptions;

    public WebhookController(
        ITelegramBotClient botClient,
        ITelegramUpdateProcessor updateProcessor,
        IOptions<TelegramOptions> telegramOptions)
    {
        _botClient = botClient;
        _updateProcessor = updateProcessor;
        _telegramOptions = telegramOptions.Value;
    }
    
    [HttpPost("{secret}")]
    public async Task<IActionResult> ReceiveUpdate(
        [FromRoute] string secret,
        [FromBody] Update update,
        CancellationToken cancellationToken)
    {
        if (secret != _telegramOptions.WebhookSecret)
        {
            return Unauthorized();
        }

        await _updateProcessor.ProcessAsync(
            _botClient,
            update,
            cancellationToken);

        return Ok();
    }
}

