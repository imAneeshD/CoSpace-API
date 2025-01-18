using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Commands.UserRoleCommands
{
    public record DeleteUserRoleCommand(int id) : IRequest<bool>;

    public class DeleteUserRoleCommandHandler(IUserRoleRepository UserRoleRepository) : IRequestHandler<DeleteUserRoleCommand, bool>
    {
        public async Task<bool> Handle(DeleteUserRoleCommand request, CancellationToken cancellationToken)
        {
            return await UserRoleRepository.DeleteUserRole(request.id);
        }
    }
}
