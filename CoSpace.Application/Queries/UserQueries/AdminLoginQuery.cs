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
    public record AdminLoginQuery(string Email, string Password) : IRequest<User>;
    class AdminLoginQueryHandler(IUserRepository adminRepository) : IRequestHandler<AdminLoginQuery, User>
    {
        public async Task<User> Handle(AdminLoginQuery request, CancellationToken cancellationToken)
        {
            return await adminRepository.Login(request.Email, request.Password);
        }
    }
}
