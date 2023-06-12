using LibraryAPI.Application.Commands.IncreaseSupplyVolume;
using MediatR;

namespace LibraryAPI.Application.Handlers.LibraryCatalogAggregate;

public class IncreaseSupplyVolumeCommandHandler : IRequestHandler<IncreaseSupplyVolumeCommand, Unit>
{
    public Task<Unit> Handle(IncreaseSupplyVolumeCommand request, CancellationToken cancellationToken)
    {
        request.Items.Select(lib =>
        {
            lib.Address = request.Address;
            return lib;
        }).ToList();

        return Task.FromResult(Unit.Value);
    }
}