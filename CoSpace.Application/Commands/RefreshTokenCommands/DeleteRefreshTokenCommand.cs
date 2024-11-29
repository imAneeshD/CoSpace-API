using CoSpace.Core.Interface;
using MediatR;

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
