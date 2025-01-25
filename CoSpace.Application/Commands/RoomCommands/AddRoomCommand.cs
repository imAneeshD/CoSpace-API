using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Commands.RoomCommands
{
    public record AddRoomCommand(Room Room) : IRequest<Room>;

    public class AddRoomCommandHandler(IRoomRepository RoomRepository) 
        : IRequestHandler<AddRoomCommand, Room>
    {
        public async Task<Room> Handle(AddRoomCommand request, CancellationToken cancellationToken)
        {
            return await RoomRepository.AddRoom(request.Room);
        }
    }
}
