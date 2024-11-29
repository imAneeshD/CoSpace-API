namespace CoSpace.API.Services.Interface
{
    public interface ITokenService
    {
        string GenerateAccessToken(string email, int id, int? orgId, int roleId);
    }
}
