using CoSpace.Core.Entities;

namespace CoSpace.Core.Interface
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken);
        Task<RefreshToken> GetRefreshTokenAsync(string refreshToken);
        Task<RefreshToken> UpdateRefreshToken(RefreshToken refreshToken);
        Task<bool> DeleteRefreshToken();
    }
}
