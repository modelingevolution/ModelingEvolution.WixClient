using System.Text.Json.Serialization;

namespace ModelingEvolution.WixClient.Models;

public class Category
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("label")]
    public string Label { get; set; } = string.Empty;

    [JsonPropertyName("postCount")]
    public int PostCount { get; set; }

    [JsonPropertyName("slug")]
    public string? Slug { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("rank")]
    public int? Rank { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("seoData")]
    public SeoData? SeoData { get; set; }

    [JsonPropertyName("coverMedia")]
    public PostMedia? CoverMedia { get; set; }

    [JsonPropertyName("language")]
    public string? Language { get; set; }

    [JsonPropertyName("translationId")]
    public string? TranslationId { get; set; }
}

public class CategoryList
{
    [JsonPropertyName("categories")]
    public List<Category> Categories { get; set; } = new();

    [JsonPropertyName("metaData")]
    public MetaData? MetaData { get; set; }
}