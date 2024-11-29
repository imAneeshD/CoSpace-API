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
    public record GetUserByIdQuery(int id) : IRequest<User>;

    public class GetUserByIdQueryHandler(IUserRepository userRepository)
        : IRequestHandler<GetUserByIdQuery, User>
    {
        public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await userRepository.GetUserById(request.id);
        }
    }
}
