using Tenki.Models;

namespace Tenki.Services;

public class StravaWebhookProcessor : IStravaWebhookProcessor
{
    private readonly ILogger<StravaWebhookProcessor> _logger;

    public StravaWebhookProcessor(ILogger<StravaWebhookProcessor> logger)
    {
        _logger = logger;
    }

    public async Task ProcessWebhookEventAsync(StravaWebhookEvent webhookEvent)
    {
        _logger.LogInformation(
            "Processing {ObjectType} webhook event: {AspectType} for ID {ObjectId}",
            webhookEvent.Objecttype,
            webhookEvent.AspectType,
            webhookEvent.Objecttype
            );

        switch (webhookEvent.Objecttype.ToLower())
        {
            case "activity":
                await HandleActivityEventAsync(webhookEvent);
                break;
            case "athlete":
                await HandleAthleteEventAsync(webhookEvent);
                break;
            default:
                break;
        }
    }

    private async Task HandleActivityEventAsync(StravaWebhookEvent webhookEvent)
    {
        switch (webhookEvent.AspectType.ToLower())
        {
            case "create":
                await HandleActivityCreatedAsync(webhookEvent);
                break;
            case "update":
                break;
            case "delete":
                break;
            default:
                break;
        }
    }

    private async Task HandleAthleteEventAsync(StravaWebhookEvent webhookEvent)
    {
        // Handle athlete deauthorization
        if (webhookEvent.AspectType.ToLower() == "update" &&
            webhookEvent.Updates.ContainsKey("authorized") &&
            webhookEvent.Updates["authorized"] == "false")
        {
            await HandleAthleteDeauthorizedAsync(webhookEvent);
        }
    }

    private Task HandleActivityCreatedAsync(StravaWebhookEvent webhookEvent)
    {
        _logger.LogInformation("Activity {ActivityId} created by athlete {AthleteId}",
            webhookEvent.ObjectId, webhookEvent.OwnerId);
        // TODO: Add activity creation handling logic here
        return Task.CompletedTask;
    }

    private Task HandleAthleteDeauthorizedAsync(StravaWebhookEvent webhookEvent)
    {
        _logger.LogInformation("Athlete {AthleteId} deauthorized the application",
            webhookEvent.OwnerId);
        // TODO: add athlete deauthorization handling logic here
        return Task.CompletedTask;
    }
    
}