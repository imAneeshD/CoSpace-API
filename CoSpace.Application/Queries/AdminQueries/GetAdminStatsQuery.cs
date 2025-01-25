using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Queries.AdminQueries
{
    public record GetAdminStatsQuery() : IRequest<DashboardStats>;

    public class GetAdminStatsQueryHandler(IAdminRepository adminRepository)
        : IRequestHandler<GetAdminStatsQuery, DashboardStats>
    {
        public async Task<DashboardStats> Handle(GetAdminStatsQuery request, CancellationToken cancellationToken)
        {
            return await adminRepository.GetAdminStats();
        }
    }
}
