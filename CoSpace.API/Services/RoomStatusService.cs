using CoSpace.API.Services.Interface;
using CoSpace.Application.Commands.RoomCommands;
using CoSpace.Application.Queries.BookingQueries;
using CoSpace.Application.Queries.RoomQueries;
using MediatR;

namespace CoSpace.API.Services
{
    public class RoomStatusService(ISender sender) : IRoomStatusService
    {

        public async Task UpdateRoomStatusAsync(int roomId)
        {
            // Get room details
            var room = await sender.Send(new GetRoomByIdQuery(roomId));
            if (room == null) return;
            var roomStatus = room.Status;

            // Get all bookings for the room
            var bookings = await sender.Send(new GetAllBookingQuery());

            // Check if any expired bookings exist
            var expiredBookings = bookings
                .Where(b => b.RoomId == roomId && b.EndTime < DateTime.UtcNow && b.Status.ToLower() == "confirmed")
                .ToList();

            // If expired bookings exist, update room status
            if (expiredBookings.Any() && room.Status.ToLower()!= "under maintenance")
            {
                room.Status = "Available";
                await sender.Send(new UpdateRoomCommand(room));
            }
            else if(room.Status.ToLower() == "under maintenance")
            {
                room.Status = "Under Maintenance";
                await sender.Send(new UpdateRoomCommand(room));
            }
            else
            {
                //room.Status = room.Status;
                //await sender.Send(new UpdateRoomCommand(room));
            }
        }
    }
}
