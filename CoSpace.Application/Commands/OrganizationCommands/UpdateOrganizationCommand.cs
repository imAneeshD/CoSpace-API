using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Commands.OrganizationCommands
{
    public record UpdateOrganizationCommand(Organization Organization) : IRequest<bool>;

    public class UpdateOrganizationHandler(IOrganizationRepository organizationRepository) :
        IRequestHandler<UpdateOrganizationCommand, bool>
    {
        public async Task<bool> Handle(UpdateOrganizationCommand request, CancellationToken cancellationToken)
        {
            return await organizationRepository.UpdateOrganization(request.Organization);
        }
    }
}
