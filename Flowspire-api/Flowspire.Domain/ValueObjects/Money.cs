namespace Flowspire.Domain.ValueObjects;

public sealed class Money : IEquatable<Money>
{
    public decimal Value { get; }

    private Money() { }

    public Money(decimal value)
    {
        if (value < 0)
            throw new ArgumentException("Money value cannot be negative.", nameof(value));

        Value = decimal.Round(value, 2, MidpointRounding.AwayFromZero);
    }

    public static Money Create(decimal value) => new Money(value);
    public static Money Zero => new Money(0m);
    public Money Add(Money other) => new Money(Value + other.Value);
    public Money Subtract(Money other) => new Money(Value - other.Value);
    public Money Multiply(decimal m) => new Money(Value * m);
    public Money Divide(decimal d)
    {
        if (d == 0) throw new DivideByZeroException();
        return new Money(Value / d);
    }

    public bool IsZero() => Value == 0;
    public bool IsPositive() => Value > 0;

    public override string ToString() => Value.ToString("C2");

    public bool Equals(Money? other)
        => other is not null && Value == other.Value;

    public override bool Equals(object? obj)
        => obj is Money m && Equals(m);

    public override int GetHashCode() => Value.GetHashCode();

    public static Money operator +(Money a, Money b) => a.Add(b);
    public static Money operator -(Money a, Money b) => a.Subtract(b);
    public static bool operator ==(Money a, Money b) => a.Equals(b);
    public static bool operator !=(Money a, Money b) => !a.Equals(b);
    public static bool operator <(Money a, Money b) => a.Value < b.Value;
    public static bool operator >(Money a, Money b) => a.Value > b.Value;
    public static bool operator <=(Money a, Money b) => a.Value <= b.Value;
    public static bool operator >=(Money a, Money b) => a.Value >= b.Value;
}
