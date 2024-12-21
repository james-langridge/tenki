using Tenki.Models;

namespace Tenki.Services;

public interface IStravaWebhookProcessor
{
    Task ProcessWebhookEventAsync(StravaWebhookEvent webhookEvent);
}
