using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Queries.RefreshTokenQueries
{
    public record GetRefreshTokenQuery(string refreshToken) : IRequest<RefreshToken>;

    public class GetGetRefreshTokenQueryHandler(IRefreshTokenRepository refreshTokenRepository)
        : IRequestHandler<GetRefreshTokenQuery, RefreshToken>
    {
        public async Task<RefreshToken> Handle(GetRefreshTokenQuery request, CancellationToken cancellationToken)
        {
            return await refreshTokenRepository.GetRefreshTokenAsync(request.refreshToken);
        }
    }
}
