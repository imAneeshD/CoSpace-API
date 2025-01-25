using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Commands.RoomCommands
{
    public record UpdateRoomCommand(Room Room) : IRequest<bool>;

    public class UpdateRoomCommandHandler(IRoomRepository RoomRepository) : IRequestHandler<UpdateRoomCommand, bool>
    {
        public async Task<bool> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            return await RoomRepository.UpdateRoom(request.Room);
        }
    }
}
