using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Commands.BookingCommands
{
    public record DeleteBookingCommand(Booking Booking) : IRequest<bool>;

    public class DeleteBookingCommandHandler(IBookingRepository BookingRepository) : IRequestHandler<DeleteBookingCommand, bool>
    {
        public async Task<bool> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
        {
            return await BookingRepository.DeleteBooking(request.Booking);
        }
    }
}
