using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Queries.HelpQueries
{
    public record GetAllHelpRequestQuery() : IRequest<IEnumerable<HelpRequest>>;

    public class GetAllHelpRequestQueryHandler(IHelpRepository helpRepository)
        : IRequestHandler<GetAllHelpRequestQuery, IEnumerable<HelpRequest>>
    {
        public async Task<IEnumerable<HelpRequest>> Handle(GetAllHelpRequestQuery request, CancellationToken cancellationToken)
        {
            return await helpRepository.GetHelpRequests();
        }
    }
}
