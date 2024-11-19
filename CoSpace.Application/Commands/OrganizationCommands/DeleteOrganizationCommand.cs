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
    public record DeleteOrganizationCommand(int id) : IRequest<bool>;

    public class DeleteOrganizationHandler(IOrganizationRepository organizationRepository) :
        IRequestHandler<DeleteOrganizationCommand, bool>
    {
        public async Task<bool> Handle(DeleteOrganizationCommand request, CancellationToken cancellationToken)
        {
            return await organizationRepository.DeleteOrganization(request.id);
        }
    }
}
