using System.Text.Json.Serialization;

namespace ModelingEvolution.WixClient.Models.Requests;

public class UpdatePostRequest
{
    [JsonPropertyName("post")]
    public PostData Post { get; set; } = new();

    [JsonPropertyName("fieldMask")]
    public FieldMask? FieldMask { get; set; }
}

public class FieldMask
{
    [JsonPropertyName("paths")]
    public List<string> Paths { get; set; } = new();
}