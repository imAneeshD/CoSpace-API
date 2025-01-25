using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Commands.BookingCommands
{
    public record AddBookingCommand(Booking booking) : IRequest<Booking>;

    public class AddBookingCommandHandler(IBookingRepository BookingRepository) 
        : IRequestHandler<AddBookingCommand, Booking>
    {
        public async Task<Booking> Handle(AddBookingCommand request, CancellationToken cancellationToken)
        {
            return await BookingRepository.AddBooking(request.booking);
        }
    }
}
