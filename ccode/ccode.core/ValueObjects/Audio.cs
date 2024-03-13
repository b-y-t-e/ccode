using System.Globalization;
using ccode.core.Interfaces;

namespace ccode.core.ValueObjects;

public readonly struct Audio : IEquatable<Audio>, IValueObject<byte[]>
{
    public byte[] Value { get; }

    ////////////////////////////////
    public Audio(byte[] value)
        => Value = value;

    ////////////////////////////////

    public static Audio From(byte[] value)
    {
        return new Audio(value);
    }

    ////////////////////////////////

    public static implicit operator byte[](Audio self)
        => self.Value;

    ////////////////////////////////

    public static bool operator ==(Audio value1, Audio value2)
        => isEqual(value1.Value, value2.Value);

    public static bool operator !=(Audio value1, Audio value2)
        => !isEqual(value1.Value, value2.Value);

    ////////////////////////////////

    public bool Equals(Audio other) =>
        isEqual(Value, other.Value);

    public override bool Equals(object obj) =>
        obj is Audio other && Equals(other);

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
