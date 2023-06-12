using LibraryAPI.Application.Commands.CreateLibraryCatalog;
using LibraryAPI.Domain.AggregationModels.LibraryCatalogAggregate;
using MediatR;

namespace LibraryAPI.Application.Handlers.LibraryCatalogAggregate;

public class CreateLibraryCatalogCommandHandler : IRequestHandler<CreateLibraryCatalogCommand, long>
{
    public Task<long> Handle(CreateLibraryCatalogCommand request, CancellationToken cancellationToken)
    {
        var newLibraryCatalog = new LibraryCatalog
        (
            new CollectionSize(request.CollectionSize),
            new ReadersPerMonth(request.ReadersPerMonth),
            new SupplyVolume(request.SupplyVolume),
            new Address(request.Address)
        );
        
        return Task.FromResult(newLibraryCatalog.Id);
    }
}