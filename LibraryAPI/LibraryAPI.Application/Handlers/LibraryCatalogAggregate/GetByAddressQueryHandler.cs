using LibraryAPI.Application.Models;
using LibraryAPI.Application.Queries.LibraryCatalogAggregate;
using LibraryAPI.Application.Queries.LibraryCatalogAggregate.Responses;
using MediatR;

namespace LibraryAPI.Application.Handlers.LibraryCatalogAggregate;

public class GetByAddressQueryHandler : IRequestHandler<GetByAddressQuery, GetByAddressQueryResponse>
{
    public Task<GetByAddressQueryResponse> Handle(GetByAddressQuery request, CancellationToken cancellationToken)
    {
        var response = new GetByAddressQueryResponse()
        {
            Item = new LibraryCatalogDto
            {
                CollectionSize = 15000,
                Address = request.Address,
                ReadersPerMonth = 1000,
                SupplyVolume = 50
            }
        };

        return Task.FromResult(response);
    }
}