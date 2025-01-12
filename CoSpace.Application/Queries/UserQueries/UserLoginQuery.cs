using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Queries.UserQueries
{
    public record UsersLoginQuery(string Email, string Password, string OrgId ) : IRequest<User>;
    class UsersLoginQueryHandler(IUserRepository usersRepository) : IRequestHandler<UsersLoginQuery, User>
    {
        public async Task<User> Handle(UsersLoginQuery request, CancellationToken cancellationToken)
        {
            return await usersRepository.Login(request.Email, request.Password, request.OrgId);
        }
    }
}
