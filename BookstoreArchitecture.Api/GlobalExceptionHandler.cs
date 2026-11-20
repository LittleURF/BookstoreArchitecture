using BookstoreArchitecture.Application.Exceptions;
using BookstoreArchitecture.Domain.Aggregates.BookAggregate;
using Microsoft.AspNetCore.Diagnostics;
using System.Data.Common;

namespace BookstoreArchitecture.Api
{
    public class GlobalExceptionHandler(IProblemDetailsService problemDetailsService, IHostEnvironment hostEnvironment) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var (status, title) = exception switch
            {
                BookNotFoundException => (StatusCodes.Status404NotFound, "Book not found"),
                InvalidPagesCountException => (StatusCodes.Status500InternalServerError, "Invalid amount of pages in a book"),
                DbException => (StatusCodes.Status500InternalServerError, "Db error"),
                _ => (StatusCodes.Status500InternalServerError, "Unknown internal error"),
            };

            // if status 500 - log error (dont always log exceptions you expect in a standard application flow)

            httpContext.Response.StatusCode = status;

            return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext()
            {
                HttpContext = httpContext,
                ProblemDetails = new()
                {
                    Status = status,
                    Title = title,
                    Detail = exception.Message
                },
                Exception = hostEnvironment.IsDevelopment() ? exception : null,
            });
        }
    }
}
