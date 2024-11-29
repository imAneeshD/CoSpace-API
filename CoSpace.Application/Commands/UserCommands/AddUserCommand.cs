using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Commands.UserCommands
{
    public record AddUserCommand(User Users) : IRequest<User>;

    public class AddUsersCommandHandler(IUserRepository UsersRepository)
        : IRequestHandler<AddUserCommand, User>
    {

        public async Task<User> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {

            return await UsersRepository.AddUser(request.Users);
        }
    }
}
