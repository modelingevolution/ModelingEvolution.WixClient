using System.Text.Json.Serialization;
using ModelingEvolution.WixClient.Identifiers;

namespace ModelingEvolution.WixClient.Models;

public class BlogPost
{
    [JsonPropertyName("id")]
    public PostId Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;

    [JsonPropertyName("excerpt")]
    public string? Excerpt { get; set; }

    [JsonPropertyName("slug")]
    public string? Slug { get; set; }

    [JsonPropertyName("featured")]
    public bool Featured { get; set; }

    [JsonPropertyName("categoryIds")]
    public List<CategoryId> CategoryIds { get; set; } = new();

    [JsonPropertyName("tagIds")]
    public List<TagId> TagIds { get; set; } = new();

    [JsonPropertyName("memberId")]
    public string? MemberId { get; set; }

    [JsonPropertyName("hashtags")]
    public List<string> Hashtags { get; set; } = new();

    [JsonPropertyName("commentingEnabled")]
    public bool CommentingEnabled { get; set; } = true;

    [JsonPropertyName("minutesToRead")]
    public int? MinutesToRead { get; set; }

    [JsonPropertyName("translationId")]
    public string? TranslationId { get; set; }

    [JsonPropertyName("language")]
    public string? Language { get; set; }

    [JsonPropertyName("createdDate")]
    public DateTime? CreatedDate { get; set; }

    [JsonPropertyName("lastUpdated")]
    public DateTime? LastUpdated { get; set; }

    [JsonPropertyName("publishedDate")]
    public DateTime? PublishedDate { get; set; }

    [JsonPropertyName("firstPublishedDate")]
    public DateTime? FirstPublishedDate { get; set; }

    [JsonPropertyName("media")]
    public PostMedia? Media { get; set; }

    [JsonPropertyName("seoData")]
    public SeoData? SeoData { get; set; }
}

public class PostMedia
{
    [JsonPropertyName("wixMedia")]
    public WixMedia? WixMedia { get; set; }

    [JsonPropertyName("displayed")]
    public bool Displayed { get; set; }
}

public class WixMedia
{
    [JsonPropertyName("image")]
    public string? Image { get; set; }

    [JsonPropertyName("video")]
    public string? Video { get; set; }
}

public class SeoData
{
    [JsonPropertyName("tags")]
    public List<SeoTag> Tags { get; set; } = new();
}

public class SeoTag
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("props")]
    public Dictionary<string, string> Props { get; set; } = new();

    [JsonPropertyName("custom")]
    public bool Custom { get; set; }

    [JsonPropertyName("disabled")]
    public bool Disabled { get; set; }
}

public class BlogPostList
{
    [JsonPropertyName("posts")]
    public List<BlogPost> Posts { get; set; } = new();

    [JsonPropertyName("metaData")]
    public MetaData? MetaData { get; set; }
}

public class MetaData
{
    [JsonPropertyName("count")]
    public int Count { get; set; }

    [JsonPropertyName("offset")]
    public int Offset { get; set; }

    [JsonPropertyName("total")]
    public int Total { get; set; }
}