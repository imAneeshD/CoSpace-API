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
    public record DeleteRefreshTokenCommand() : IRequest<bool>;

    public class DeleteRefreshTokenCommandHandler(IRefreshTokenRepository refreshTokenRepository) : IRequestHandler<DeleteRefreshTokenCommand, bool>
    {
        public async Task<bool> Handle(DeleteRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            return await refreshTokenRepository.DeleteRefreshToken();
        }
    }
}
