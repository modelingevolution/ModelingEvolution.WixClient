using System.Text.Json.Serialization;

namespace ModelingEvolution.WixClient.Models.Requests;

public class UpdateDraftPostRequest
{
    [JsonPropertyName("draftPost")]
    public DraftPostData DraftPost { get; set; } = new();

    [JsonPropertyName("fieldMask")]
    public FieldMask? FieldMask { get; set; }
}