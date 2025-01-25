using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Commands.NotificationCommands
{
    public record UpdateNotificationCommand(Notification Notification) : IRequest<bool>;

    public class UpdateNotificationCommandHandler(INotificationRepository NotificationRepository) : IRequestHandler<UpdateNotificationCommand, bool>
    {
        public async Task<bool> Handle(UpdateNotificationCommand request, CancellationToken cancellationToken)
        {
            return await NotificationRepository.UpdateNotification(request.Notification);
        }
    }
}
