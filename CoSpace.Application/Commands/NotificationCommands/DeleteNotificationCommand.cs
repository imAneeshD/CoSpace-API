using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Commands.NotificationCommands
{
    public record DeleteNotificationCommand(int id) : IRequest<bool>;

    public class DeleteNotificationCommandHandler(INotificationRepository NotificationRepository) : IRequestHandler<DeleteNotificationCommand, bool>
    {
        public async Task<bool> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
        {
            return await NotificationRepository.DeleteNotification(request.id);
        }
    }
}
