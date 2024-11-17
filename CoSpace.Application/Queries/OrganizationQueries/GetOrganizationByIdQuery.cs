using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSpace.Application.Queries.OrganizationQueries
{
    public record GetOrganizationByIdQuery(int id) : IRequest<Organization>;

    public class GetOrganizationByIdQueryQueryHandler(IOrganizationRepository organizationRepository)
        : IRequestHandler<GetOrganizationByIdQuery, Organization>
    {
        public async Task<Organization> Handle(GetOrganizationByIdQuery request, CancellationToken cancellationToken)
        {
            return await organizationRepository.GetOrganizationById(request.id);
        }
    }
}
