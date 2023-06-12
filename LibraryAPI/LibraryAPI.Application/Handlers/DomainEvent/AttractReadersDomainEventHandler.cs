using LibraryAPI.Domain.Events;
using MediatR;
using Serilog;

namespace LibraryAPI.Application.Handlers.DomainEvent;

public class AttractReadersDomainEventHandler: INotificationHandler<AttractReadersDomainEvent>
{
    public Task Handle(AttractReadersDomainEvent notification, CancellationToken cancellationToken)
    {
        Log.Information($"Теперь библиотеку по адресу '{notification.Address.Value}' посещают в месяц {notification.ReadersPerMonth.Value} человек.");
        
        return Task.CompletedTask;
    }
}