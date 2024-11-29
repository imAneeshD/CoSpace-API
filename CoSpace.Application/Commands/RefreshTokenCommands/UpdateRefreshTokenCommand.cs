using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Commands.RefreshTokenCommands
{
    public record UpdateRefreshTokenCommand(RefreshToken refreshToken) : IRequest<RefreshToken>;

    public class UpdateRefreshTokenCommandHandler(IRefreshTokenRepository refreshTokenRepository) : IRequestHandler<UpdateRefreshTokenCommand, RefreshToken>
    {
        public async Task<RefreshToken> Handle(UpdateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            return await refreshTokenRepository.UpdateRefreshToken(request.refreshToken);
        }
    }
}
