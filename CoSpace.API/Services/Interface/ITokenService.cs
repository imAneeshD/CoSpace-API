namespace CoSpace.API.Services.Interface
{
    public interface ITokenService
    {
        string GenerateAccessToken(string email, int userType, int id, int orgId, int appUserTypeId);
        public string GenerateRefreshToken();
        Task SaveRefreshToken(int userId, string refreshToken, int appUserTypeId);
        Task<string> RefreshAccessToken(string refreshToken);
    }
}
