using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSpace.Application.Queries.AdminQueries
{
    public record AdminLoginQuery(string Email, string Password) : IRequest<Admin>;
    class AdminLoginQueryHandler(IAdminRepository adminRepository) : IRequestHandler<AdminLoginQuery, Admin>
    {
        public async Task<Admin> Handle(AdminLoginQuery request, CancellationToken cancellationToken)
        {
            return await adminRepository.Login(request.Email, request.Password);
        }
    }
}
