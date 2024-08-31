using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSpace.Application.Queries
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
