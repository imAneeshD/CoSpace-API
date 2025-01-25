using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Commands.OrganizationCommands
{
    public record DeleteOrganizationCommand(Organization Organization) : IRequest<bool>;

    public class DeleteOrganizationHandler(IOrganizationRepository organizationRepository) :
        IRequestHandler<DeleteOrganizationCommand, bool>
    {
        public async Task<bool> Handle(DeleteOrganizationCommand request, CancellationToken cancellationToken)
        {
            return await organizationRepository.DeleteOrganization(request.Organization);
        }
    }
}
