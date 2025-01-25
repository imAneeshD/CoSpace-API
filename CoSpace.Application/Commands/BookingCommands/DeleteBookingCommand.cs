using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Commands.BookingCommands
{
    public record DeleteBookingCommand(int id) : IRequest<bool>;

    public class DeleteBookingCommandHandler(IBookingRepository BookingRepository) : IRequestHandler<DeleteBookingCommand, bool>
    {
        public async Task<bool> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
        {
            return await BookingRepository.DeleteBooking(request.id);
        }
    }
}
