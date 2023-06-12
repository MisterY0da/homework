using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.AggregationModels.LibraryCatalogAggregate;

public class CollectionSize : ValueObject
{
    public int Value { get; }
        
    public CollectionSize(int value)
    {
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}