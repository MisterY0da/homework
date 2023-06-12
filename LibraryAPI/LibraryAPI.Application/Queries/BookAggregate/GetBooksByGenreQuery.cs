using LibraryAPI.Application.Queries.BookAggregate.Responses;
using MediatR;

namespace LibraryAPI.Application.Queries.BookAggregate;

public class GetBooksByGenreQuery: IRequest<GetBooksByGenreQueryResponse>
{
    /// <summary>
    /// Genre
    /// </summary>
    public string Genre { get; set; }
}