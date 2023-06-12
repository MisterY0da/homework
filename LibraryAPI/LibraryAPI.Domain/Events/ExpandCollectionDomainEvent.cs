using LibraryAPI.Domain.AggregationModels.LibraryCatalogAggregate;
using MediatR;

namespace LibraryAPI.Domain.Events;

public class ExpandCollectionDomainEvent : INotification
{
    public CollectionSize CollectionSize { get; private set; }

    public ReadersPerMonth ReadersPerMonth { get; private set; }

    public SupplyVolume SupplyVolume { get; private set; }

    public Address Address { get; private set; }
    
    public ExpandCollectionDomainEvent(CollectionSize collectionSize, ReadersPerMonth readersPerMonth, SupplyVolume supplyVolume, Address address)
    {
        CollectionSize = collectionSize;
        ReadersPerMonth = readersPerMonth;
        SupplyVolume = supplyVolume;
        Address = address;
    }

}