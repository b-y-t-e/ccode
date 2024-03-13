namespace ccode.core.Interfaces;

public interface IValueObject<T> : IValueObject
{
    T Value { get; }
}

public interface IValueObject
{
}
