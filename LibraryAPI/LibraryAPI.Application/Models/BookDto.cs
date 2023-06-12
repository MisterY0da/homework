using LibraryAPI.Domain.AggregationModels.BookAggregate;

namespace LibraryAPI.Application.Models;

public class BookDto
{
    public string AuthorName { get; set; }
    public string Genre { get; set; }
    public string Title { get; set; }
    public int Edition { get; set; }
}