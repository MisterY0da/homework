using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Events;

namespace LibraryAPI.Domain.AggregationModels.LibraryCatalogAggregate;

public class LibraryCatalog : Entity, IAggregationRoot
{
    public LibraryCatalog(CollectionSize collectionSize, ReadersPerMonth readersPerMonth, SupplyVolume supplyVolume, Address address)
    {
        CollectionSize = collectionSize;
        ReadersPerMonth = readersPerMonth;
        SupplyVolume = supplyVolume;
        Address = address;
    }

    public CollectionSize CollectionSize { get; private set; }

    public ReadersPerMonth ReadersPerMonth { get; private set; }

    public SupplyVolume SupplyVolume { get; private set; }

    public Address Address { get; private set; }

    public void IncreaseCollection(int booksAmount)
    {
        this.CollectionSize = new CollectionSize(CollectionSize.Value + booksAmount);
        AddExpandCollectionDomainEvent(this.CollectionSize, this.ReadersPerMonth, this.SupplyVolume, this.Address);
    }

    public void IncreaseReadersCount(int readersAmount)
    {
        this.ReadersPerMonth = new ReadersPerMonth(ReadersPerMonth.Value + readersAmount);
        AddAttractReadersDomainEvent(this.CollectionSize, this.ReadersPerMonth, this.SupplyVolume, this.Address);
    }
    
    public void AddAttractReadersDomainEvent(CollectionSize collectionSize, ReadersPerMonth readersPerMonth,
        SupplyVolume supplyVolume, Address address)
    {
        var attractReadersDomainEvent = new AttractReadersDomainEvent(collectionSize, readersPerMonth, supplyVolume, address);
        AddDomainEvent(attractReadersDomainEvent);
    }
    
    public void AddExpandCollectionDomainEvent(CollectionSize collectionSize, ReadersPerMonth readersPerMonth,
        SupplyVolume supplyVolume, Address address)
    {
        var expandCollectionDomainEvent =
            new ExpandCollectionDomainEvent(collectionSize, readersPerMonth, supplyVolume, address);
        AddDomainEvent(expandCollectionDomainEvent);
    }
}