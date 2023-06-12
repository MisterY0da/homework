using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.AggregationModels.BookAggregate;

public class Edition : ValueObject
{
    public int Value { get; }
        
    public Edition(int value)
    {
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}