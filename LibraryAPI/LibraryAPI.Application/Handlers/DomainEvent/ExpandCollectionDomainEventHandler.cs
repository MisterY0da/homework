using LibraryAPI.Domain.Events;
using MediatR;
using Serilog;

namespace LibraryAPI.Application.Handlers.DomainEvent;

public class ExpandCollectionDomainEventHandler: INotificationHandler<ExpandCollectionDomainEvent>
{
    public Task Handle(ExpandCollectionDomainEvent notification, CancellationToken cancellationToken)
    {
        Log.Information($"Коллекция библиотеки по адресу '{notification.Address.Value}' теперь составляет {notification.CollectionSize.Value} книг.");
        
        return Task.CompletedTask;
    }
}