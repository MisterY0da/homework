using LibraryAPI.Application.Queries.BookAggregate.Responses;
using LibraryAPI.Domain.AggregationModels.BookAggregate;
using MediatR;

namespace LibraryAPI.Application.Queries.BookAggregate;

public class GetBooksByAuthorQuery : IRequest<GetBooksByAuthorQueryResponse>
{
    /// <summary>
    /// Author
    /// </summary>
    public string AuthorName { get; set; }
}