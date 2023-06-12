using LibraryAPI.Application.Models;
using LibraryAPI.Application.Queries.BookAggregate;
using LibraryAPI.Application.Queries.BookAggregate.Responses;
using LibraryAPI.Domain.AggregationModels.BookAggregate;
using MediatR;

namespace LibraryAPI.Application.Handlers.BookAggregate;

public class GetBooksByGenreQueryHandler : IRequestHandler<GetBooksByGenreQuery, GetBooksByGenreQueryResponse>
{
    public Task<GetBooksByGenreQueryResponse> Handle(GetBooksByGenreQuery request, CancellationToken cancellationToken)
    {
        var response = new GetBooksByGenreQueryResponse()
        {
            Items = new List<BookDto>()
            {
                new BookDto()
                {
                    AuthorName = "name1",
                    Edition = 3,
                    Genre = request.Genre,
                    Title = "Title1"
                },
                new BookDto()
                {
                    AuthorName = "name2",
                    Edition = 5,
                    Genre = request.Genre,
                    Title = "Title2"
                }
            }
        };

        return Task.FromResult(response);
    }
}