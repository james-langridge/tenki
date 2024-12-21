using System.Text.Json.Serialization;

namespace Tenki.Models;

public class StravaWebhookValidation
{
    [JsonPropertyName("hub.mode")] public string Mode { get; set; } = string.Empty;
    
    [JsonPropertyName("hub.challenge")]
    public string Challenege { get; set; } = string.Empty;
    
    [JsonPropertyName("hub.verify_token")]
    public string VerifyToken { get; set; } = string.Empty;
}