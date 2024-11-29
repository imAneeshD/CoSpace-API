using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Queries.UserQueries
{
    public record GetUsersQuery() : IRequest<IEnumerable<User>>;

    public class GetUsersQueryHandler(IUserRepository UserRepository)
        : IRequestHandler<GetUsersQuery, IEnumerable<User>>
    {
        public async Task<IEnumerable<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return await UserRepository.GetUsers();
        }
    }
}
