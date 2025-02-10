using Core.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.Test.EventHandlers
{
    public class CreatedTestEventHandler(ILogger<CreatedTestEventHandler> logger) 
        : INotificationHandler<CreatedNewTestEvent>
    {
        public Task Handle(CreatedNewTestEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Have been save test case have id {0} and name is {1}", notification.Id, notification.Name);
            return Task.CompletedTask;
        }
    }
}