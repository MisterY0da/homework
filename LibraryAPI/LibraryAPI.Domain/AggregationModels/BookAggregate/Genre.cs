using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.AggregationModels.BookAggregate;

public class Genre: ValueObject
{
    public string Value { get; }
        
    public Genre(string value)
    {
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}