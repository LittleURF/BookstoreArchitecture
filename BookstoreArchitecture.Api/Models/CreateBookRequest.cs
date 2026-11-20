namespace BookstoreArchitecture.Api.Models;

public sealed record CreateBookRequest(
    string Title,
    int PageCount,
    decimal Price,
    string CurrencyCode);
