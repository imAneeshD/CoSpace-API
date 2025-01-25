using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Queries.CanteenMenuQueries
{
    public record GetAllCanteenMenuQuery() : IRequest<IEnumerable<CanteenMenu>>;

    public class GetAllCanteenMenuQueryHandler(ICanteenRepository canteenRepository)
        : IRequestHandler<GetAllCanteenMenuQuery, IEnumerable<CanteenMenu>>
    {
        public async Task<IEnumerable<CanteenMenu>> Handle(GetAllCanteenMenuQuery request, CancellationToken cancellationToken)
        {
            return await canteenRepository.GetCanteenMenus();
        }
    }
}
