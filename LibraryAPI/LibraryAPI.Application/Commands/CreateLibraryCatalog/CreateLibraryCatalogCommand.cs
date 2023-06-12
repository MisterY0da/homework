using MediatR;

namespace LibraryAPI.Application.Commands.CreateLibraryCatalog;

public class CreateLibraryCatalogCommand :  IRequest<long>
{
    public int CollectionSize { get; set; }
    public int ReadersPerMonth { get; set; }
    public int SupplyVolume { get; set; }
    public string Address { get; set; }
}