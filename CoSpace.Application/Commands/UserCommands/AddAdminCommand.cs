using AutoMapper;
using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSpace.Application.Commands.AdminCommand
{
    public record AddAdminCommand(User Admin) : IRequest<User>;

    public class AddAdminCommandHandler(IUserRepository adminRepository)
        : IRequestHandler<AddAdminCommand, User>
    {

        public async Task<User> Handle(AddAdminCommand request, CancellationToken cancellationToken)
        {

            return await adminRepository.AddAdmin(request.Admin);
        }
    }
}
