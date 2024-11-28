using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSpace.Application.Commands.UsersCommand
{
    public record UpdateUsersCommand(User Users) : IRequest<bool>;

    public class UpdateUsersCommandHandler(IUserRepository UsersRepository)
        : IRequestHandler<UpdateUsersCommand, bool>
    {
        public async Task<bool> Handle(UpdateUsersCommand request, CancellationToken cancellationToken)
        {
            return await UsersRepository.UpdateUser(request.Users);
        }
    }
}
