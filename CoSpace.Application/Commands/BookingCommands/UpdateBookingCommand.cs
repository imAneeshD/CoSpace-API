using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Commands.BookingCommands
{
    public record UpdateHelpRequestCommand(Booking Booking) : IRequest<bool>;

    public class UpdateBookingCommandHandler(IBookingRepository BookingRepository) : IRequestHandler<UpdateHelpRequestCommand, bool>
    {
        public async Task<bool> Handle(UpdateHelpRequestCommand request, CancellationToken cancellationToken)
        {
            return await BookingRepository.UpdateBooking(request.Booking);
        }
    }
}
