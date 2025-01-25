using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Commands.UserCommands
{
    public record DeleteUserCommand(User user) : IRequest<bool>;

    public class DeleteUsersCommandHandler(IUserRepository UsersRepository)
        : IRequestHandler<DeleteUserCommand, bool>
    {
        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            return await UsersRepository.DeleteUser(request.user);
        }
    }
}
