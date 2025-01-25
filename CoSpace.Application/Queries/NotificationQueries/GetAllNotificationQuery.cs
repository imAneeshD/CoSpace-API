using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Queries.NotificationQueries
{
    public record GetAllNotificationQuery() : IRequest<IEnumerable<Notification>>;

    public class GetAllNotificationQueryHandler(INotificationRepository NotificationRepository)
        : IRequestHandler<GetAllNotificationQuery, IEnumerable<Notification>>
    {
        public async Task<IEnumerable<Notification>> Handle(GetAllNotificationQuery request, CancellationToken cancellationToken)
        {
            return await NotificationRepository.GetNotifications();
        }
    }
}
