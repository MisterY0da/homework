using LibraryAPI.Application.Queries.LibraryCatalogAggregate.Responses;
using MediatR;

namespace LibraryAPI.Application.Queries.LibraryCatalogAggregate;

public class GetByAddressQuery: IRequest<GetByAddressQueryResponse>
{
    /// <summary>
    /// Address
    /// </summary>
    public string Address { get; set; }
}