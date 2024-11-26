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
    public record UpdateAdminCommand(User Admin) : IRequest<bool>;

    public class UpdateAdminCommandHandler(IUserRepository adminRepository)
        : IRequestHandler<UpdateAdminCommand, bool>
    {
        public async Task<bool> Handle(UpdateAdminCommand request, CancellationToken cancellationToken)
        {
            return await adminRepository.UpdateUser(request.Admin);
        }
    }
}
