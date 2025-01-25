using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Queries.OrganizationQueries
{
    public record GetOrganizationByNameQuery(string name) : IRequest<Organization>;

    public class GetOrganizationByNameQueryQueryHandler(IOrganizationRepository organizationRepository)
        : IRequestHandler<GetOrganizationByNameQuery, Organization>
    {
        public async Task<Organization> Handle(GetOrganizationByNameQuery request, CancellationToken cancellationToken)
        {
            return await organizationRepository.GetOrganizationByName(request.name);
        }
    }
}
