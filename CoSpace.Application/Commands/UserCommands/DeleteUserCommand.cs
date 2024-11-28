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
    public record DeleteUsersCommand(int Id) : IRequest<bool>;

    public class DeleteUsersCommandHandler(IUserRepository UsersRepository)
        : IRequestHandler<DeleteUsersCommand, bool>
    {
        public async Task<bool> Handle(DeleteUsersCommand request, CancellationToken cancellationToken)
        {
            return await UsersRepository.DeleteUser(request.Id);
        }
    }
}
