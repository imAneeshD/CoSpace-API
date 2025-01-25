using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Commands.BookingCommands
{
    public record AddHelpRequestCommand(Booking booking) : IRequest<Booking>;

    public class AddBookingCommandHandler(IBookingRepository BookingRepository) 
        : IRequestHandler<AddHelpRequestCommand, Booking>
    {
        public async Task<Booking> Handle(AddHelpRequestCommand request, CancellationToken cancellationToken)
        {
            return await BookingRepository.AddBooking(request.booking);
        }
    }
}
