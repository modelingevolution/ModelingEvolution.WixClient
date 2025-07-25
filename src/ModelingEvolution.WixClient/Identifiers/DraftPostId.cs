using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using ModelingEvolution.JsonParsableConverter;

namespace ModelingEvolution.WixClient.Identifiers;

/// <summary>
/// Strongly typed identifier for a Wix Draft Post
/// </summary>
[JsonConverter(typeof(JsonParsableConverter<DraftPostId>))]
public readonly record struct DraftPostId : 
    IEquatable<DraftPostId>, 
    IComparable<DraftPostId>,
    IComparable, 
    IParsable<DraftPostId>
{
    public string Value { get; }

    public DraftPostId(string value)
    {
        Value = value;
    }

    public static implicit operator string(DraftPostId id) => id.Value;
    public static implicit operator DraftPostId(string value) => new(value);

    public static readonly DraftPostId Empty = default;

    public override string ToString() => Value;

    public int CompareTo(DraftPostId other) => 
        string.Compare(Value, other.Value, StringComparison.Ordinal);

    public int CompareTo(object? obj)
    {
        if (obj is null) return 1;
        return obj is DraftPostId other 
            ? CompareTo(other) 
            : throw new ArgumentException($"Object must be of type {nameof(DraftPostId)}");
    }

    public static DraftPostId Parse(string s, IFormatProvider? provider)
    {
        if (TryParse(s, provider, out var result))
            return result;
        return Empty;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, out DraftPostId result)
    {
        result = default;
        
        if (string.IsNullOrWhiteSpace(s))
            return false;

        result = new DraftPostId(s);
        return true;
    }
}