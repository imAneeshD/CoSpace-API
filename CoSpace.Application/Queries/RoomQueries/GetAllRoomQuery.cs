using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Queries.RoomQueries
{
    public record GetAllRoomQuery() : IRequest<IEnumerable<Room>>;

    public class GetAllRoomQueryHandler(IRoomRepository RoomRepository)
        : IRequestHandler<GetAllRoomQuery, IEnumerable<Room>>
    {
        public async Task<IEnumerable<Room>> Handle(GetAllRoomQuery request, CancellationToken cancellationToken)
        {
            return await RoomRepository.GetRooms();
        }
    }
}
