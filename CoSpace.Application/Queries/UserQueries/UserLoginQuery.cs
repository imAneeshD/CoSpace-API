using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSpace.Application.Queries.UsersQueries
{
    public record UsersLoginQuery(string Email, string Password) : IRequest<User>;
    class UsersLoginQueryHandler(IUserRepository UsersRepository) : IRequestHandler<UsersLoginQuery, User>
    {
        public async Task<User> Handle(UsersLoginQuery request, CancellationToken cancellationToken)
        {
            return await UsersRepository.Login(request.Email, request.Password);
        }
    }
}
