using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSpace.Application.Queries.UserQueries
{
    public record GetUsersQuery() : IRequest<IEnumerable<User>>;

    public class GetUsersQueryHandler(IUserRepository adminRepository)
        : IRequestHandler<GetUsersQuery, IEnumerable<User>>
    {
        public async Task<IEnumerable<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return await adminRepository.GetUsers();
        }
    }
}
