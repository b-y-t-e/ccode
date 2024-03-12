using System;
using System.Globalization;
using ccode.core.Interfaces;

namespace ccode.core.ValueObjects;

public readonly struct Prompt : IEquatable<Prompt>, IValueObject<String>
{
    public String Value { get; }

    ////////////////////////////////
    public Prompt(String value)
        => Value = value;

    ////////////////////////////////

    public static Prompt From(String value)
    {
        return new Prompt(value);
    }

    ////////////////////////////////

    public static implicit operator String(Prompt self)
        => self.Value;

    ////////////////////////////////

    public static bool operator ==(Prompt value1, Prompt value2)
        => isEqual(value1.Value, value2.Value);

    public static bool operator !=(Prompt value1, Prompt value2)
        => !isEqual(value1.Value, value2.Value);

    ////////////////////////////////

    public bool Equals(Prompt other) =>
        isEqual(Value, other.Value);

    public override bool Equals(object obj) =>
        obj is Prompt other && Equals(other);

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
