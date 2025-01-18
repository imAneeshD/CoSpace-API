using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Queries.UserRoleQueries
{
    public record GetUserRoleByIdQuery(int id) : IRequest<UserRole>;

    public class GetUserRoleByIdQueryQueryHandler(IUserRoleRepository UserRoleRepository)
        : IRequestHandler<GetUserRoleByIdQuery, UserRole>
    {
        public async Task<UserRole> Handle(GetUserRoleByIdQuery request, CancellationToken cancellationToken)
        {
            return await UserRoleRepository.GetUserRole(request.id);
        }
    }
}
