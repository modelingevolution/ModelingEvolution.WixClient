using System.Text.Json.Serialization;

namespace ModelingEvolution.WixClient.Models.Requests;

public class ListPostsRequest
{
    [JsonPropertyName("paging")]
    public Paging? Paging { get; set; }

    [JsonPropertyName("filter")]
    public object? Filter { get; set; }

    [JsonPropertyName("sort")]
    public List<Sort>? Sort { get; set; }

    [JsonPropertyName("fieldsToInclude")]
    public List<string>? FieldsToInclude { get; set; }

    [JsonPropertyName("language")]
    public string? Language { get; set; }
}

public class Paging
{
    [JsonPropertyName("limit")]
    public int? Limit { get; set; }

    [JsonPropertyName("offset")]
    public int? Offset { get; set; }
}

public class Sort
{
    [JsonPropertyName("fieldName")]
    public string FieldName { get; set; } = string.Empty;

    [JsonPropertyName("order")]
    public string Order { get; set; } = "ASC"; // ASC or DESC
}