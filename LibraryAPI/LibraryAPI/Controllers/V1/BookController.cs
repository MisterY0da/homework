using LibraryAPI.Application.Commands.CreateBook;
using LibraryAPI.Application.Queries.BookAggregate;
using LibraryAPI.Application.Queries.BookAggregate.Responses;
using LibraryAPI.Domain.AggregationModels.BookAggregate;
using LibraryAPI.Domain.Events;
using LibraryAPI.HttpModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace LibraryAPI.Controllers.V1;

[ApiController]
[Route("v1/api/books")]
[Produces("application/json")]
public class BookController : ControllerBase
{
    private readonly IMediator _mediator;

    public BookController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("byauthor")]
    public async Task<GetBooksByAuthorQueryResponse> GetByAuthor(string authorName, CancellationToken token)
    {
        return await _mediator.Send(new GetBooksByAuthorQuery{AuthorName = authorName}, token);
    }
    
    [HttpGet("bygenre")]
    public async Task<GetBooksByGenreQueryResponse> GetByGenre(string genre, CancellationToken token)
    {
        return await _mediator.Send(new GetBooksByGenreQuery{Genre = genre}, token);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Add(BookViewModel viewModel, CancellationToken token)
    {
        var createBookCommand = new CreateBookCommand()
        {
            AuthorName = viewModel.AuthorName,
            Edition = viewModel.Edition,
            Genre = viewModel.Genre,
            Title = viewModel.Title
        };
        
        var result = await _mediator.Send(createBookCommand, token);

        return Ok(result);
    }

    [HttpPost("newedition")]
    public async Task<ActionResult<Unit>> RenewEdition(string title, int edition, string authorName, CancellationToken token)
    {
        var book = new Book(
            new Author(authorName), 
            new Edition(edition), 
            new Genre("genre1"), 
            new Title(title)
            );
        
        book.RenewEdition();
        await _mediator.Publish(new RenewEditionDomainEvent(book.Author, book.Edition, book.Genre, book.Title));
        return Ok();
    }
}