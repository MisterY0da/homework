using LibraryAPI.Domain.AggregationModels.BookAggregate;
using MediatR;

namespace LibraryAPI.Application.Commands.CreateBook;

public class CreateBookCommand : IRequest<long>
{
    public string AuthorName { get; set; }
    public string Genre { get; set; }
    public string Title { get; set; }
    public int Edition { get; set; }
}