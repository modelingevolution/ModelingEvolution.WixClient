using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using ModelingEvolution.JsonParsableConverter;

namespace ModelingEvolution.WixClient.Identifiers;

/// <summary>
/// Strongly typed identifier for a Wix Blog Tag
/// </summary>
[JsonConverter(typeof(JsonParsableConverter<TagId>))]
public readonly record struct TagId : 
    IEquatable<TagId>, 
    IComparable<TagId>,
    IComparable, 
    IParsable<TagId>
{
    public string Value { get; }

    public TagId(string value)
    {
        Value = value;
    }

    public static implicit operator string(TagId id) => id.Value;
    public static implicit operator TagId(string value) => new(value);

    public static readonly TagId Empty = default;

    public override string ToString() => Value;

    public int CompareTo(TagId other) => 
        string.Compare(Value, other.Value, StringComparison.Ordinal);

    public int CompareTo(object? obj)
    {
        if (obj is null) return 1;
        return obj is TagId other 
            ? CompareTo(other) 
            : throw new ArgumentException($"Object must be of type {nameof(TagId)}");
    }

    public static TagId Parse(string s, IFormatProvider? provider)
    {
        if (TryParse(s, provider, out var result))
            return result;
        return Empty;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, out TagId result)
    {
        result = default;
        
        if (string.IsNullOrWhiteSpace(s))
            return false;

        result = new TagId(s);
        return true;
    }
}