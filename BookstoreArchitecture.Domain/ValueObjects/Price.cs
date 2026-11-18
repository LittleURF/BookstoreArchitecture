namespace BookstoreArchitecture.Domain.ValueObjects;

public sealed record Price
{
    public decimal Amount { get; }
    public string CurrencyCode { get; }

    public Price(decimal amount, string currencyCode)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Price cannot be negative.");
        }

        if (currencyCode is null || currencyCode.Length != 3 ||
            !currencyCode.All(character =>
                character is >= 'A' and <= 'Z' or >= 'a' and <= 'z'))
        {
            throw new ArgumentException("Currency code must contain three letters.", nameof(currencyCode));
        }

        Amount = amount;
        CurrencyCode = currencyCode.ToUpperInvariant();
    }
}
