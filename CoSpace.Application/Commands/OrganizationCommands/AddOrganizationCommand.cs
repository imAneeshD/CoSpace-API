using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSpace.Application.Commands.OrganizationCommands
{
    public record AddOrganizationCommand(Organization Organization) : IRequest<Organization>;

    public class AddOrganizationCommandHandler(IOrganizationRepository organizationRepository) : IRequestHandler<AddOrganizationCommand, Organization>
    {
        public async Task<Organization> Handle(AddOrganizationCommand request, CancellationToken cancellationToken)
        {
            return await organizationRepository.AddOrganization(request.Organization);
        }
    }
}
