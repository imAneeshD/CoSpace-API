using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Commands.UserRoleCommands
{
    public record UpdateUserRoleCommand(UserRole UserRole) : IRequest<bool>;

    public class UpdateUserRoleCommandHandler(IUserRoleRepository UserRoleRepository) : IRequestHandler<UpdateUserRoleCommand, bool>
    {
        public async Task<bool> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            return await UserRoleRepository.UpdateUserRole(request.UserRole);
        }
    }
}
