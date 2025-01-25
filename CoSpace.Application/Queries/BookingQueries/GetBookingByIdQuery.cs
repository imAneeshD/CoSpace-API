using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Queries.BookingQueries
{
    public record GetBookingByIdQuery(int id) : IRequest<Booking>;

    public class GetBookingByIdQueryQueryHandler(IBookingRepository BookingRepository)
        : IRequestHandler<GetBookingByIdQuery, Booking>
    {
        public async Task<Booking> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
        {
            return await BookingRepository.GetBookingById(request.id);
        }
    }
}
