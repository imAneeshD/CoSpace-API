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
    public record GetUsersByIdQuery(int id) : IRequest<User>;

    public class GetUsersByIdQueryHandler(IUserRepository userRepository)
        : IRequestHandler<GetUsersByIdQuery, User>
    {
        public async Task<User> Handle(GetUsersByIdQuery request, CancellationToken cancellationToken)
        {
            return await userRepository.GetUserById(request.id);
        }
    }
}
