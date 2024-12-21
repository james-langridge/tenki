namespace Tenki.Models;

public class StravaWebhookEvent
{
    public string Objecttype { get; set; } = string.Empty;
    public long ObjectId { get; set; }
    public string AspectType { get; set; } = string.Empty;
    public Dictionary<string, string> Updates { get; set; } = new();
    public long OwnerId { get; set; }
    public int SubscriptionId { get; set; }
    public long EventTime { get; set; }
}