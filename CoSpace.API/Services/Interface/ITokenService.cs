namespace CoSpace.API.Services.Interface
{
    public interface ITokenService
    {
        string GenerateAccessToken(string email, int id, int? orgId, int roleId);
        public string GenerateRefreshToken();
        Task SaveRefreshToken(int userId, string refreshToken);
        Task<string> RefreshAccessToken(string refreshToken);
    }
}
