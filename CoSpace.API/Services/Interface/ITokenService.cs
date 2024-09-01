namespace CoSpace.API.Services.Interface
{
    public interface ITokenService
    {
        string GenerateToken(string username);
    }
}
