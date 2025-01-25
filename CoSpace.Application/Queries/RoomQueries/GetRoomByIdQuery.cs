using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Queries.RoomQueries
{
    public record GetRoomByIdQuery(int id) : IRequest<Room>;

    public class GetRoomByIdQueryQueryHandler(IRoomRepository RoomRepository)
        : IRequestHandler<GetRoomByIdQuery, Room>
    {
        public async Task<Room> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
        {
            return await RoomRepository.GetRoomById(request.id);
        }
    }
}
