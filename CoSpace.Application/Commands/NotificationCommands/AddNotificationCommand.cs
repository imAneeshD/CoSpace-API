using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Commands.NotificationCommands
{
    public record AddNotificationCommand(Notification Notification) : IRequest<Notification>;

    public class AddNotificationCommandHandler(INotificationRepository NotificationRepository) 
        : IRequestHandler<AddNotificationCommand, Notification>
    {
        public async Task<Notification> Handle(AddNotificationCommand request, CancellationToken cancellationToken)
        {
            return await NotificationRepository.AddNotification(request.Notification);
        }
    }
}
