using LibraryAPI.Application.Commands.CreateLibraryCatalog;
using LibraryAPI.Application.Queries.LibraryCatalogAggregate;
using LibraryAPI.Application.Queries.LibraryCatalogAggregate.Responses;
using LibraryAPI.Domain.AggregationModels.LibraryCatalogAggregate;
using LibraryAPI.Domain.Events;
using LibraryAPI.HttpModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers.V1;

[ApiController]
[Route("v1/api/library")]
[Produces("application/json")]
public class LibraryCatalogController : ControllerBase
{
    private readonly IMediator _mediator;

    public LibraryCatalogController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("byaddress")]
    public async Task<GetByAddressQueryResponse> GetByAddress(string address, CancellationToken token)
    {
        return await _mediator.Send(new GetByAddressQuery{ Address = address}, token);
    }
    
    [HttpPost]
    public async Task<ActionResult<int>> Add(LibraryViewModel viewModel, CancellationToken token)
    {
        var createLibraryCommand = new CreateLibraryCatalogCommand
        {
            CollectionSize = viewModel.CollectionSize,
            ReadersPerMonth = viewModel.ReadersPerMonth,
            SupplyVolume = viewModel.SupplyVolume,
            Address = viewModel.Address
        };
        
        var result = await _mediator.Send(createLibraryCommand, token);
        return Ok(result);
    }

    [HttpPost("expandcollection")]
    public async Task<ActionResult<Unit>> ExpandCollection(int booksAmount)
    {
        var catalog = new LibraryCatalog(
            new CollectionSize(10000),
            new ReadersPerMonth(100),
            new SupplyVolume(50),
            new Address("Baker St. 25")
            );
        
        catalog.IncreaseCollection(booksAmount);

        _mediator.Publish(new ExpandCollectionDomainEvent(catalog.CollectionSize, catalog.ReadersPerMonth,
            catalog.SupplyVolume, catalog.Address));
        return Ok();
    }
    
    [HttpPost("attractreaders")]
    public async Task<ActionResult<Unit>> AttractREaders(int readersAmount)
    {
        var catalog = new LibraryCatalog(
            new CollectionSize(10000),
            new ReadersPerMonth(100),
            new SupplyVolume(50),
            new Address("Baker St. 25")
        );
        
        catalog.IncreaseReadersCount(readersAmount);

        _mediator.Publish(new AttractReadersDomainEvent(catalog.CollectionSize, catalog.ReadersPerMonth,
            catalog.SupplyVolume, catalog.Address));
        return Ok();
    }
}