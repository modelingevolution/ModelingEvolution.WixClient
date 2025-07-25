using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using ModelingEvolution.JsonParsableConverter;

namespace ModelingEvolution.WixClient.Identifiers;

/// <summary>
/// Strongly typed identifier for a Wix Blog Post
/// </summary>
[JsonConverter(typeof(JsonParsableConverter<PostId>))]
public readonly record struct PostId : 
    IEquatable<PostId>, 
    IComparable<PostId>,
    IComparable, 
    IParsable<PostId>
{
    public string Value { get; }

    public PostId(string value)
    {
        Value = value;
    }

    public static implicit operator string(PostId id) => id.Value;
    public static implicit operator PostId(string value) => new(value);

    public static readonly PostId Empty = default;

    public override string ToString() => Value;

    public int CompareTo(PostId other) => 
        string.Compare(Value, other.Value, StringComparison.Ordinal);

    public int CompareTo(object? obj)
    {
        if (obj is null) return 1;
        return obj is PostId other 
            ? CompareTo(other) 
            : throw new ArgumentException($"Object must be of type {nameof(PostId)}");
    }

    public static PostId Parse(string s, IFormatProvider? provider)
    {
        if (TryParse(s, provider, out var result))
            return result;
        return Empty;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, out PostId result)
    {
        result = default;
        
        if (string.IsNullOrWhiteSpace(s))
            return false;

        result = new PostId(s);
        return true;
    }
}