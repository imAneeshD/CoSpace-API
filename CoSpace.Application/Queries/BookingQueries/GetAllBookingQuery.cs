using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Queries.BookingQueries
{
    public record GetAllHelpRequestQuery() : IRequest<IEnumerable<Booking>>;

    public class GetAllBookingQueryHandler(IBookingRepository BookingRepository)
        : IRequestHandler<GetAllHelpRequestQuery, IEnumerable<Booking>>
    {
        public async Task<IEnumerable<Booking>> Handle(GetAllHelpRequestQuery request, CancellationToken cancellationToken)
        {
            return await BookingRepository.GetBookings();
        }
    }
}
