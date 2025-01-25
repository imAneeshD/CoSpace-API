using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Queries.HelpQueries
{
    public record GetHelpRequestByIdQuery(int id) : IRequest<HelpRequest>;

    public class GetHelpRequestByIdQueryQueryHandler(IHelpRepository helpRepository)
        : IRequestHandler<GetHelpRequestByIdQuery, HelpRequest>
    {
        public async Task<HelpRequest> Handle(GetHelpRequestByIdQuery request, CancellationToken cancellationToken)
        {
            return await helpRepository.GetHelpRequestById(request.id);
        }
    }
}
