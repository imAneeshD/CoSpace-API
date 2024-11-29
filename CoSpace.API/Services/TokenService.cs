using CoSpace.API.Services.Interface;
using CoSpace.Application.Commands.RefreshTokenCommands;
using CoSpace.Application.Queries.AdminQueries;
using CoSpace.Application.Queries.RefreshTokenQueries;
using CoSpace.Core.Entities;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CoSpace.API.Services
{
    public class TokenService(ISender sender, IConfiguration configuration) : ITokenService
    {

        public string GenerateAccessToken(string email, int id, int? OrgId, int roleId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("Id", id.ToString()),
                    new Claim("OrgId", OrgId.ToString()),
                    new Claim(ClaimTypes.Role, roleId.ToString()),
                }),
                Expires = DateTime.Now.AddHours(1),
                Issuer = configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }

        public async Task SaveRefreshToken(int userId, string refreshToken)
        {
            var token = new RefreshToken
            {
                UserId = userId,
                Token = refreshToken,
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };
            var result = await sender.Send(new AddRefreshTokenCommand(token));
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
                newAccessToken = GenerateAccessToken(admin.Email, admin.Id, admin.OrganizationId, admin.RoleId);
            }
            else
            {
                var user = await sender.Send(new GetAdminByIdQuery(existingRefreshToken.UserId));
                newAccessToken = GenerateAccessToken(user.Email, user.Id, user.OrganizationId, user.RoleId);
            }
            return newAccessToken;
        }
    }
}
