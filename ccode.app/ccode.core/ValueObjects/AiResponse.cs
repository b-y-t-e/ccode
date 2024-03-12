using System;
using System.Globalization;
using ccode.core.Interfaces;

namespace ccode.core.ValueObjects;

public readonly struct AiResponse : IEquatable<AiResponse>, IValueObject<String>
{
    public String Value { get; }

    ////////////////////////////////
    public AiResponse(String value)
        => Value = value;

    ////////////////////////////////

    public static AiResponse From(String value)
    {
        return new AiResponse(value);
    }

    ////////////////////////////////

    public static implicit operator String(AiResponse self)
        => self.Value;

    ////////////////////////////////

    public static bool operator ==(AiResponse value1, AiResponse value2)
        => isEqual(value1.Value, value2.Value);

    public static bool operator !=(AiResponse value1, AiResponse value2)
        => !isEqual(value1.Value, value2.Value);

    ////////////////////////////////

    public bool Equals(AiResponse other) =>
        isEqual(Value, other.Value);

    public override bool Equals(object obj) =>
        obj is AiResponse other && Equals(other);

    ////////////////////////////////

    public override int GetHashCode() => Value.GetHashCode();

    //public override string ToString() =>
    //    Convert.ToString(Value, System.Globalization.CultureInfo.InvariantCulture);

    public override string ToString() =>
        Convert.ToString(Value, CultureInfo.InvariantCulture);

    ////////////////////////////////

    static bool isEqual(object v1, object v2)
    {
        if (v1 == null && v2 == null) return true;
        else if (v1 != null && v2 == null) return false;
        else if (v1 == null && v2 != null) return false;
        else return v1.Equals(v2);
    }
}
