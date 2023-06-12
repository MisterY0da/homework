using LibraryAPI.Domain.AggregationModels.BookAggregate;
using MediatR;

namespace LibraryAPI.Domain.Events;

/// <summary>
/// Переиздана книга
/// </summary>
public class RenewEditionDomainEvent : INotification
{
    public Author Author { get; private set; }
    
    public Edition Edition { get; private set; }

    public Genre Genre { get; private set; }

    public Title Title { get; private set; }

    public RenewEditionDomainEvent(Author author, Edition edition, Genre genre, Title title)
    {
        Author = author;
        Edition = edition;
        Genre = genre;
        Title = title;
    }
}