using LibraryAPI.Domain.Common;
using LibraryAPI.Domain.Events;

namespace LibraryAPI.Domain.AggregationModels.BookAggregate;

public class Book : Entity, IAggregationRoot
{
    public Author Author { get; private set; }
    
    public Edition Edition { get; private set; }

    public Genre Genre { get; private set; }

    public Title Title { get; private set; }

    public Book(Author author, Edition edition, Genre genre, Title title)
    {
        Author = author;
        Edition = edition;
        Genre = genre;
        Title = title;
    }

    public void RenewEdition()
    {
        this.Edition = new Edition(this.Edition.Value + 1);
        AddRenewEditionDomainEvent(this.Author, this.Edition, this.Genre, this.Title);
    }

    private void AddRenewEditionDomainEvent(Author author, Edition edition, Genre genre, Title title)
    {
        var renewEditionDomainEvent = new RenewEditionDomainEvent(author, edition, genre, title);
        AddDomainEvent(renewEditionDomainEvent);
    }
}