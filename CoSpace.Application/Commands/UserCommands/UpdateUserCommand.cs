using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Commands.UserCommands
{
    public record UpdateUserCommand(User Users) : IRequest<bool>;

    public class UpdateUserCommandHandler(IUserRepository UsersRepository)
        : IRequestHandler<UpdateUserCommand, bool>
    {
        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            return await UsersRepository.UpdateUser(request.Users);
        }
    }
}
