using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Commands.AdminCommands
{
    public record UpdateAdminCommand(Admin admin) : IRequest<bool>;

    public class UpdateAdminCommandHandler(IAdminRepository adminsRepository)
        : IRequestHandler<UpdateAdminCommand, bool>
    {
        public async Task<bool> Handle(UpdateAdminCommand request, CancellationToken cancellationToken)
        {
            return await adminsRepository.UpdateAdmin(request.admin);
        }
    }
}
