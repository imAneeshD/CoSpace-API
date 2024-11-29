using CoSpace.API.Services.Interface;
using CoSpace.Application.Commands.RefreshTokenCommands;
using CoSpace.Application.Queries.AdminQueries;
using CoSpace.Application.Queries.RefreshTokenQueries;
using CoSpace.Core.Entities;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;

namespace CoSpace.API.Services
{
    public class RefreshTokenService(ISender sender, ITokenService tokenService) : IRefreshTokenService
    {
        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }

        public async Task<RefreshToken> AddRefreshTokenAsync(string refreshToken, int userId)
        {
            var token = new RefreshToken
            {
                UserId = userId,
                Token = refreshToken,
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };
            var result = await sender.Send(new AddRefreshTokenCommand(token));
            return result;
        }

        public async Task<string> RefreshAccessToken(string refreshToken, string userType)
        {
            string newAccessToken = string.Empty;

            var existingRefreshToken = await sender.Send(new GetRefreshTokenQuery(refreshToken));

            if (existingRefreshToken == null)
            {
                throw new SecurityTokenException("Invalid or expired refresh token");
            }

            if (userType == "admin")
            {
                var admin = await sender.Send(new GetAdminByIdQuery(existingRefreshToken.UserId));
                newAccessToken = tokenService.GenerateAccessToken(admin.Email, admin.Id, admin.OrganizationId, admin.RoleId);
            }
            else
            {
                var user = await sender.Send(new GetAdminByIdQuery(existingRefreshToken.UserId));
                newAccessToken = tokenService.GenerateAccessToken(user.Email, user.Id, user.OrganizationId, user.RoleId);
            }
            return newAccessToken;
        }

        public async Task<bool> DeleteRefreshToken()
        {
            await sender.Send(new DeleteRefreshTokenCommand());
            return true;
        }

        public async Task<RefreshToken> GetRefreshToken(string token)
        {
            return await sender.Send(new GetRefreshTokenQuery(token));
        }
    }
}
