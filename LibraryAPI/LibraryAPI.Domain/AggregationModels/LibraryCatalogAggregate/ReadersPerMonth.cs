using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.AggregationModels.LibraryCatalogAggregate;

public class ReadersPerMonth : ValueObject
{
    public int Value { get; }
        
    public ReadersPerMonth(int value)
    {
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}