using System.Text.Json.Serialization;
using ModelingEvolution.WixClient.Identifiers;

namespace ModelingEvolution.WixClient.Models;

public class Tag
{
    [JsonPropertyName("id")]
    public TagId Id { get; set; }

    [JsonPropertyName("label")]
    public string Label { get; set; } = string.Empty;

    [JsonPropertyName("slug")]
    public string? Slug { get; set; }

    [JsonPropertyName("postCount")]
    public int PostCount { get; set; }

    [JsonPropertyName("publicationCount")]
    public int? PublicationCount { get; set; }

    [JsonPropertyName("createdDate")]
    public DateTime? CreatedDate { get; set; }

    [JsonPropertyName("updatedDate")]
    public DateTime? UpdatedDate { get; set; }

    [JsonPropertyName("language")]
    public string? Language { get; set; }

    [JsonPropertyName("translationId")]
    public string? TranslationId { get; set; }
}

public class TagList
{
    [JsonPropertyName("tags")]
    public List<Tag> Tags { get; set; } = new();

    [JsonPropertyName("metaData")]
    public MetaData? MetaData { get; set; }
}