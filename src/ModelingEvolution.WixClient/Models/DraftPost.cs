using System.Text.Json.Serialization;

namespace ModelingEvolution.WixClient.Models;

public class DraftPost : BlogPost
{
    [JsonPropertyName("status")]
    public string Status { get; set; } = "DRAFT";
}