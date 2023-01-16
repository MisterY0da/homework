using Microsoft.AspNetCore.Mvc;

namespace servicetwo.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
    private static readonly string[] Titles = new[]
    {
        "Crime and Punishment", "The Idiot", "Demons", "Brothers Karamazov", "The Gambler"
    };

    private readonly ILogger<BookController> _logger;

    public BookController(ILogger<BookController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetBook")]
    public IEnumerable<Book> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new Book
        {
            Author = "Fyodor Dostoevsky",
            Price = Random.Shared.Next(100, 500),
            Title = Titles[Random.Shared.Next(Titles.Length)]
        })
        .ToArray();
    }
}
