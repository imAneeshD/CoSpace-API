using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Queries.CanteenMenuQueries
{
    public record GetCanteenMenuByIdQuery(int id) : IRequest<CanteenMenu>;

    public class GetCanteenMenuByIdQueryQueryHandler(ICanteenRepository canteenRepository)
        : IRequestHandler<GetCanteenMenuByIdQuery, CanteenMenu>
    {
        public async Task<CanteenMenu> Handle(GetCanteenMenuByIdQuery request, CancellationToken cancellationToken)
        {
            return await canteenRepository.GetCanteenMenuById(request.id);
        }
    }
}
