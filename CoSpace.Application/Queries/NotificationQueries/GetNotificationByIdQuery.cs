using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Queries.NotificationQueries
{
    public record GetNotificationByIdQuery(int id) : IRequest<Notification>;

    public class GetNotificationByIdQueryQueryHandler(INotificationRepository NotificationRepository)
        : IRequestHandler<GetNotificationByIdQuery, Notification>
    {
        public async Task<Notification> Handle(GetNotificationByIdQuery request, CancellationToken cancellationToken)
        {
            return await NotificationRepository.GetNotificationById(request.id);
        }
    }
}
