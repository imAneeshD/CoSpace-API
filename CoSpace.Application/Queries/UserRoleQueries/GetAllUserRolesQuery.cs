using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Queries.UserRoleQueries
{
    public record GetAllUserRolesQuery() : IRequest<IEnumerable<UserRole>>;

    public class GetAllUserRolesQueryHandler(IUserRoleRepository userRoleRepository)
        : IRequestHandler<GetAllUserRolesQuery, IEnumerable<UserRole>>
    {
        public async Task<IEnumerable<UserRole>> Handle(GetAllUserRolesQuery request, CancellationToken cancellationToken)
        {
            return await userRoleRepository.GetUserRoles();
        }
    }
}
