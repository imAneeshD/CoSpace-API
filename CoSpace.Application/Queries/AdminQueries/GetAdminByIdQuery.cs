using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Queries.AdminQueries
{
    public record GetAdminByIdQuery(int id) : IRequest<Admin>;

    public class GetAdminByIdQueryHandler(IAdminRepository adminRepository)
        : IRequestHandler<GetAdminByIdQuery, Admin>
    {
        public async Task<Admin> Handle(GetAdminByIdQuery request, CancellationToken cancellationToken)
        {
            return await adminRepository.GetAdminById(request.id);
        }
    }
}
