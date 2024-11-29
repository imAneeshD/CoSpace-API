using CoSpace.Core.Entities;

namespace CoSpace.API.Services.Interface
{
    public interface IRefreshTokenService
    {
        Task<RefreshToken> AddRefreshTokenAsync(string refreshToken, int userId);
        Task<string> RefreshAccessToken(string refreshToken, string userType);
        public string GenerateRefreshToken();
        Task<bool> DeleteRefreshToken();
        Task<RefreshToken> GetRefreshToken(string token);

    }
}
