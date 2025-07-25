using System.Text.Json.Serialization;

namespace ModelingEvolution.WixClient.Models.Requests;

public class CreateDraftPostRequest
{
    [JsonPropertyName("draftPost")]
    public DraftPostData DraftPost { get; set; } = new();
}

public class DraftPostData : PostData
{
    // Inherits all properties from PostData
}