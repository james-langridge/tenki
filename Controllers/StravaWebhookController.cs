using Microsoft.AspNetCore.Mvc;
using Tenki.Models;
using Tenki.Services;

namespace Tenki.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StravaWebhookController : ControllerBase
{
    private readonly ILogger<StravaWebhookController> _logger;
    private readonly IStravaWebhookProcessor _webhookProcessor;
    private readonly IConfiguration _configuration;

    public StravaWebhookController(
        ILogger<StravaWebhookController> logger,
        IStravaWebhookProcessor webhookProcessor,
        IConfiguration configuration)
    {
        _logger = logger;
        _webhookProcessor = webhookProcessor;
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult HandleValidation([FromQuery] StravaWebhookValidation validation)
    {
        try
        {
            string expectedToken = _configuration["Strava:WebhokVerifyToken"];
            if (validation.VerifyToken != expectedToken)
            {
                _logger.LogWarning("Invalid webhook verification token received");
                return BadRequest("Invalid verification token");
            }

            var response = new StravaWebhookValidationResponse
            {
                Challenge = validation.Challenege
            };
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error handling webhook validation");
            return StatusCode(500);
        }
    }

    [HttpPost]
    public async Task<IActionResult> HandleWebhook([FromBody] StravaWebhookEvent webhookEvent)
    {
        try
        {
            _ = Task.Run(async () =>
            {
                try
                {
                    await _webhookProcessor.ProcessWebhookEventAsync((webhookEvent));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing webhook event");
                }

            });

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error handling webhook");
            return StatusCode(500);
        }
    }
    
}