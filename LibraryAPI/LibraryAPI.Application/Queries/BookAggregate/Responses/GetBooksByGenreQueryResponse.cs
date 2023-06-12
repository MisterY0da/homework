using LibraryAPI.Application.Models;

namespace LibraryAPI.Application.Queries.BookAggregate.Responses;

public class GetBooksByGenreQueryResponse: IItemsModel<BookDto>
{
    public IReadOnlyList<BookDto> Items { get; set; }
}