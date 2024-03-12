using System.Globalization;
using ccode.core.Interfaces;

namespace ccode.core.ValueObjects;

public readonly struct Transcription : IEquatable<Transcription>, IValueObject<string>
{
    public string Value { get; }

    ////////////////////////////////
    public Transcription(string value)
        => Value = value;

    ////////////////////////////////

    public static Transcription From(string value)
    {
        return new Transcription(value);
    }

    ////////////////////////////////

    public static implicit operator string(Transcription self)
        => self.Value;

    ////////////////////////////////

    public static bool operator ==(Transcription value1, Transcription value2)
        => isEqual(value1.Value, value2.Value);

    public static bool operator !=(Transcription value1, Transcription value2)
        => !isEqual(value1.Value, value2.Value);

    ////////////////////////////////

    public bool Equals(Transcription other) =>
        isEqual(Value, other.Value);

    public override bool Equals(object obj) =>
        obj is Transcription other && Equals(other);

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
