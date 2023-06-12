using LibraryAPI.Application.Models;

namespace LibraryAPI.Application.Queries.BookAggregate.Responses;

public class GetBooksByAuthorQueryResponse : IItemsModel<BookDto>
{
    public IReadOnlyList<BookDto> Items { get; set; }
}