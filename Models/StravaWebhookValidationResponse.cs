using System.Text.Json.Serialization;

namespace Tenki.Models;

public class StravaWebhookValidationResponse
{
    [JsonPropertyName("hub.challenge")] public string Challenge { get; set; } = string.Empty;
}