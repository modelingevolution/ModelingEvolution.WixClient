using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using ModelingEvolution.JsonParsableConverter;

namespace ModelingEvolution.WixClient.Identifiers;

/// <summary>
/// Strongly typed identifier for a Wix Blog Category
/// </summary>
[JsonConverter(typeof(JsonParsableConverter<CategoryId>))]
public readonly record struct CategoryId : 
    IEquatable<CategoryId>, 
    IComparable<CategoryId>,
    IComparable, 
    IParsable<CategoryId>
{
    public string Value { get; }

    public CategoryId(string value)
    {
        Value = value;
    }

    public static implicit operator string(CategoryId id) => id.Value;
    public static implicit operator CategoryId(string value) => new(value);

    public static readonly CategoryId Empty = default;

    public override string ToString() => Value;

    public int CompareTo(CategoryId other) => 
        string.Compare(Value, other.Value, StringComparison.Ordinal);

    public int CompareTo(object? obj)
    {
        if (obj is null) return 1;
        return obj is CategoryId other 
            ? CompareTo(other) 
            : throw new ArgumentException($"Object must be of type {nameof(CategoryId)}");
    }

    public static CategoryId Parse(string s, IFormatProvider? provider)
    {
        if (TryParse(s, provider, out var result))
            return result;
        return Empty;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, out CategoryId result)
    {
        result = default;
        
        if (string.IsNullOrWhiteSpace(s))
            return false;

        result = new CategoryId(s);
        return true;
    }
}