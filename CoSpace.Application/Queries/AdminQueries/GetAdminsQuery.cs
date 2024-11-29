using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Queries.AdminQueries
{
    public record GetAdminsQuery() : IRequest<IEnumerable<Admin>>;

    public class GetAdminsQueryHandler(IAdminRepository adminRepository)
        : IRequestHandler<GetAdminsQuery, IEnumerable<Admin>>
    {
        public async Task<IEnumerable<Admin>> Handle(GetAdminsQuery request, CancellationToken cancellationToken)
        {
            return await adminRepository.GetAdmins();
        }
    }
}
