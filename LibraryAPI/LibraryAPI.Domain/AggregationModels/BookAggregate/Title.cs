using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.AggregationModels.BookAggregate;

public class Title: ValueObject
{
    public string Value { get; }
        
    public Title(string value)
    {
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}