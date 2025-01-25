using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Queries.BookingQueries
{
    public record GetHelpRequestByIdQuery(int id) : IRequest<Booking>;

    public class GetBookingByIdQueryQueryHandler(IBookingRepository BookingRepository)
        : IRequestHandler<GetHelpRequestByIdQuery, Booking>
    {
        public async Task<Booking> Handle(GetHelpRequestByIdQuery request, CancellationToken cancellationToken)
        {
            return await BookingRepository.GetBookingById(request.id);
        }
    }
}
