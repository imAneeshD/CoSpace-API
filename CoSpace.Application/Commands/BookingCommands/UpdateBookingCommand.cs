using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Commands.BookingCommands
{
    public record UpdateBookingCommand(Booking Booking) : IRequest<bool>;

    public class UpdateBookingCommandHandler(IBookingRepository BookingRepository) : IRequestHandler<UpdateBookingCommand, bool>
    {
        public async Task<bool> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
        {
            return await BookingRepository.UpdateBooking(request.Booking);
        }
    }
}
