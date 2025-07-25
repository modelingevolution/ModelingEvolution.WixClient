using System.Text.Json.Serialization;
using ModelingEvolution.WixClient.Identifiers;

namespace ModelingEvolution.WixClient.Models.Requests;

public class CreatePostRequest
{
    [JsonPropertyName("post")]
    public PostData Post { get; set; } = new();
}

public class PostData
{
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;

    [JsonPropertyName("excerpt")]
    public string? Excerpt { get; set; }

    [JsonPropertyName("featured")]
    public bool Featured { get; set; }

    [JsonPropertyName("categoryIds")]
    public List<CategoryId>? CategoryIds { get; set; }

    [JsonPropertyName("tagIds")]
    public List<TagId>? TagIds { get; set; }

    [JsonPropertyName("memberId")]
    public string? MemberId { get; set; }

    [JsonPropertyName("hashtags")]
    public List<string>? Hashtags { get; set; }

    [JsonPropertyName("commentingEnabled")]
    public bool CommentingEnabled { get; set; } = true;

    [JsonPropertyName("media")]
    public PostMedia? Media { get; set; }

    [JsonPropertyName("seoData")]
    public SeoData? SeoData { get; set; }
}