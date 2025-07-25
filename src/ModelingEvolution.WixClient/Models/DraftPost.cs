using System.Text.Json.Serialization;
using ModelingEvolution.WixClient.Identifiers;

namespace ModelingEvolution.WixClient.Models;

public class DraftPost : BlogPost
{
    [JsonPropertyName("id")]
    public new DraftPostId Id { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; } = "DRAFT";
}