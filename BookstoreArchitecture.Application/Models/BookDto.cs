namespace BookstoreArchitecture.Application.Models;

public record BookDto(Guid Id, string Title, int PageCount, decimal Price, string CurrencyCode);
