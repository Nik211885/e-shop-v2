using Application.Interface;
using Core.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.Test.EventHandlers
{
    public class CreatedTestEventHandler(ILogger<CreatedTestEventHandler> logger, ISendNotification sendNotification) 
        : INotificationHandler<CreatedNewTestEvent>
    {
        public async Task Handle(CreatedNewTestEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Have been save test case have id {0} and name is {1}", notification.Id, notification.Name);
            var body = "Test case has id {0} and name is {1}";
            await sendNotification.SendAsync("finally", string.Format(body, notification.Id, notification.Name), "testCase", "Test");
        }
    }
}