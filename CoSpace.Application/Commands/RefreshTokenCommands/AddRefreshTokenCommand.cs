using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSpace.Application.Commands.RefreshTokenCommands
{
    public record AddRefreshTokenCommand(RefreshToken refreshToken) : IRequest<RefreshToken>;

    public class AddRefreshTokenCommandHandler(IRefreshTokenRepository refreshTokenRepository) : IRequestHandler<AddRefreshTokenCommand, RefreshToken>
    {
        public async Task<RefreshToken> Handle(AddRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            return await refreshTokenRepository.AddRefreshToken(request.refreshToken);
        }
    }
}
