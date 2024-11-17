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
    public record GetAllOrganizationsQuery() : IRequest<IEnumerable<Organization>>;

    public class GetAllOrganizationsQueryHandler(IOrganizationRepository organizationRepository)
        : IRequestHandler<GetAllOrganizationsQuery, IEnumerable<Organization>>
    {
        public async Task<IEnumerable<Organization>> Handle(GetAllOrganizationsQuery request, CancellationToken cancellationToken)
        {
            return await organizationRepository.GetOrganizations();
        }
    }
}
