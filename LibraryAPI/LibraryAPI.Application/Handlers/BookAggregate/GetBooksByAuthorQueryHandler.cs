using LibraryAPI.Application.Models;
using LibraryAPI.Application.Queries.BookAggregate;
using LibraryAPI.Application.Queries.BookAggregate.Responses;
using MediatR;

namespace LibraryAPI.Application.Handlers.BookAggregate;

public class GetBooksByAuthorQueryHandler : IRequestHandler<GetBooksByAuthorQuery, GetBooksByAuthorQueryResponse>
{
    public Task<GetBooksByAuthorQueryResponse> Handle(GetBooksByAuthorQuery request, CancellationToken cancellationToken)
    {
        var response = new GetBooksByAuthorQueryResponse()
        {
            Items = new List<BookDto>()
            {
                new BookDto()
                {
                    AuthorName = request.AuthorName,
                    Edition = 1,
                    Genre = "Genre1",
                    Title = "Title1"
                },
                new BookDto()
                {
                    AuthorName = request.AuthorName,
                    Edition = 2,
                    Genre = "Genre2",
                    Title = "Title2"
                },
                new BookDto()
                {
                    AuthorName = request.AuthorName,
                    Edition = 3,
                    Genre = "Genre3",
                    Title = "Title3"
                }
            }
        };

        return Task.FromResult(response);
    }
}