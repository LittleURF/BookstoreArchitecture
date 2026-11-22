namespace BookstoreArchitecture.Application.DTOs;

public record BookDto(Guid Id, string Title, int PageCount, decimal Price);
