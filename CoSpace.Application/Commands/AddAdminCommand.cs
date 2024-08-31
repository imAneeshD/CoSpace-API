using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSpace.Application.Commands
{
    public record AddAdminCommand(Admin Admin) : IRequest<Admin>;


    public class AddAdminCommandHandler(IAdminRepository adminRepository)
        : IRequestHandler<AddAdminCommand, Admin>
    {
        public async Task<Admin> Handle(AddAdminCommand request, CancellationToken cancellationToken)
        {
           return await adminRepository.AddAdmin(request.Admin);
        }
    }
}
