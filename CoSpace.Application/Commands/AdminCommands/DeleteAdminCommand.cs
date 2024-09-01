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
    public record DeleteAdminCommand(int Id) : IRequest<bool>;

    public class DeleteAdminCommandHandler(IAdminRepository adminRepository)
        : IRequestHandler<DeleteAdminCommand, bool>
    {
        public async Task<bool> Handle(DeleteAdminCommand request, CancellationToken cancellationToken)
        {
            return await adminRepository.DeleteAdmin(request.Id);
        }
    }
}
