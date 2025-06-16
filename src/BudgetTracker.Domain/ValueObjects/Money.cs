using BudgetTracker.Domain.Enums;

namespace BudgetTracker.Domain.ValueObjects;

public sealed class Money : IEquatable<Money>
{
    public Money(decimal amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public decimal Amount { get; }

    public Currency Currency { get; }

    public static Money Zero(Currency currency) => new(0, currency);

    public Money Add(Money other)
    {
        ValidateSameCurrency(other);

        return new Money(Amount + other.Amount, Currency);
    }

    public Money Subtract(Money other)
    {
        ValidateSameCurrency(other);

        return new Money(Amount - other.Amount, Currency);
    }

    public bool IsGreaterThan(Money other)
    {
        ValidateSameCurrency(other);

        return Amount > other.Amount;
    }

    private void ValidateSameCurrency(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException("Cannot perform operations on different currencies");
    }

    public bool Equals(Money? other)
    {
        return other is not null && Amount == other.Amount && Currency == other.Currency;
    }

    public override bool Equals(object? obj)
    {
        return obj is Money other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Amount, Currency);
    }

    public static bool operator ==(Money left, Money right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Money left, Money right)
    {
        return !Equals(left, right);
    }
}
