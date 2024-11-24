namespace CoSpace.API.Services.Interface
{
    public interface ITokenService
    {
        string GenerateAccessToken(string email, string userType, int id, int orgId);
    }
}
