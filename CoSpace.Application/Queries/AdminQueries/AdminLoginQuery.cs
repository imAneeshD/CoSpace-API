using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Queries.AdminQueries
{
    public record AdminsLoginQuery(string Email, string Password) : IRequest<Admin>;
    class AdminsLoginQueryHandler(IAdminRepository adminsRepository) : IRequestHandler<AdminsLoginQuery, Admin>
    {
        public async Task<Admin> Handle(AdminsLoginQuery request, CancellationToken cancellationToken)
        {
            return await adminsRepository.Login(request.Email, request.Password);
        }
    }
}
