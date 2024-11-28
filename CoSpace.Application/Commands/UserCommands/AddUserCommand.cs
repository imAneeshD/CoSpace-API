using AutoMapper;
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
    public record AddUsersCommand(User Users) : IRequest<User>;

    public class AddUsersCommandHandler(IUserRepository UsersRepository)
        : IRequestHandler<AddUsersCommand, User>
    {

        public async Task<User> Handle(AddUsersCommand request, CancellationToken cancellationToken)
        {

            return await UsersRepository.AddUser(request.Users);
        }
    }
}
