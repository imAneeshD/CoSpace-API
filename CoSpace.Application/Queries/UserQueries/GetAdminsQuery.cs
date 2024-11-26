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
    public record GetAdminsQuery() : IRequest<IEnumerable<User>>;

    public class GetAdminsQueryHandler(IUserRepository adminRepository)
        : IRequestHandler<GetAdminsQuery, IEnumerable<User>>
    {
        public async Task<IEnumerable<User>> Handle(GetAdminsQuery request, CancellationToken cancellationToken)
        {
            return await adminRepository.GetUsers();
        }
    }
}
