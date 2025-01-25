using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Commands.RoomCommands
{
    public record DeleteRoomCommand(Room room) : IRequest<bool>;

    public class DeleteRoomCommandHandler(IRoomRepository RoomRepository) : IRequestHandler<DeleteRoomCommand, bool>
    {
        public async Task<bool> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            return await RoomRepository.DeleteRoom(request.room);
        }
    }
}
