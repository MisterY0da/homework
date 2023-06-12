using LibraryAPI.Domain.Common;

namespace LibraryAPI.Domain.AggregationModels.BookAggregate;

public class Author : Entity
{
    public string Name { get; }
    public Author(string name)
    {
        Name = name;
    }
}