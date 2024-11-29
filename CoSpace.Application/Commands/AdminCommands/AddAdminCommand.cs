using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Commands.AdminCommands
{
    public record AddAdminCommand(Admin admin) : IRequest<Admin>;

    public class AddAdminsCommandHandler(IAdminRepository adminsRepository)
        : IRequestHandler<AddAdminCommand, Admin>
    {

        public async Task<Admin> Handle(AddAdminCommand request, CancellationToken cancellationToken)
        {

            return await adminsRepository.AddAdmin(request.admin);
        }
    }
}
