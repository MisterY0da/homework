using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.AggregationModels.LibraryCatalogAggregate;

public class SupplyVolume: ValueObject
{
    public int Value { get; }
        
    public SupplyVolume(int value)
    {
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}