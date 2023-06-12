using LibraryAPI.Application.Models;
using MediatR;

namespace LibraryAPI.Application.Commands.IncreaseSupplyVolume;

public class IncreaseSupplyVolumeCommand : IRequest<Unit>
{
    public string Address { get; set; }

    public IList<LibraryCatalogDto> Items { get; set; }
}