using LibraryAPI.Application.Commands.CreateBook;
using LibraryAPI.Domain.AggregationModels.BookAggregate;
using MediatR;

namespace LibraryAPI.Application.Handlers.BookAggregate;

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, long>
{
    public Task<long> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var newBook = new Book(
            new Author(request.AuthorName),
            new Edition(request.Edition),
            new Genre(request.Genre),
            new Title(request.Title)
        );

        return Task.FromResult(newBook.Id);
    }
}