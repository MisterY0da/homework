using System.Diagnostics;
using LibraryAPI.Domain.Events;
using MediatR;
using Microsoft.VisualBasic;
using Serilog;

namespace LibraryAPI.Application.Handlers.DomainEvent;

public class RenewEditionDomainEventHandler : INotificationHandler<RenewEditionDomainEvent>
{
    public Task Handle(RenewEditionDomainEvent notification, CancellationToken cancellationToken)
    {
        Log.Information($"Книга '{notification.Title.Value}' автора '{notification.Author.Name}' получила издание №{notification.Edition.Value}.");

        return Task.CompletedTask;
    }
}