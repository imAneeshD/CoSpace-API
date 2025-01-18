using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Commands.UserRoleCommands
{
    public record AddUserRoleCommand(UserRole role) : IRequest<UserRole>;

    public class AddUserRoleCommandHandler(IUserRoleRepository userRoleRepository) 
        : IRequestHandler<AddUserRoleCommand, UserRole>
    {
        public async Task<UserRole> Handle(AddUserRoleCommand request, CancellationToken cancellationToken)
        {
            return await userRoleRepository.AddUserRole(request.role);
        }
    }
}
