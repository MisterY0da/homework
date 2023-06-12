using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.AggregationModels.LibraryCatalogAggregate;

public class Address: ValueObject
{
    public string Value { get; }
        
    public Address(string value)
    {
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}