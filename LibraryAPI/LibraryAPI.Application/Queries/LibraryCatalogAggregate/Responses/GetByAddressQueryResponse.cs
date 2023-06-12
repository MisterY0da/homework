using LibraryAPI.Application.Models;

namespace LibraryAPI.Application.Queries.LibraryCatalogAggregate.Responses;

public class GetByAddressQueryResponse
{
    public LibraryCatalogDto Item { get; set; }
}