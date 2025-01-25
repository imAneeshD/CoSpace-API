using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Commands.BookingCommands
{
    public record DeleteHelpRequestCommand(int id) : IRequest<bool>;

    public class DeleteBookingCommandHandler(IBookingRepository BookingRepository) : IRequestHandler<DeleteHelpRequestCommand, bool>
    {
        public async Task<bool> Handle(DeleteHelpRequestCommand request, CancellationToken cancellationToken)
        {
            return await BookingRepository.DeleteBooking(request.id);
        }
    }
}
