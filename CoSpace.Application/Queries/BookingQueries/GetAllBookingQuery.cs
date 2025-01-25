using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Queries.BookingQueries
{
    public record GetAllBookingQuery() : IRequest<IEnumerable<Booking>>;

    public class GetAllBookingQueryHandler(IBookingRepository BookingRepository)
        : IRequestHandler<GetAllBookingQuery, IEnumerable<Booking>>
    {
        public async Task<IEnumerable<Booking>> Handle(GetAllBookingQuery request, CancellationToken cancellationToken)
        {
            return await BookingRepository.GetBookings();
        }
    }
}
