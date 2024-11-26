using CoSpace.Utility.Models.Request;
using CoSpace.API.Services.Interface;
using CoSpace.Application.Commands;
using CoSpace.Application.Commands.AdminCommand;
using CoSpace.Application.Queries;
using CoSpace.Application.Queries.AdminQueries;
using CoSpace.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CoSpace.Core.DTO;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using CoSpace.Application.Queries.RefreshTokenQueries;
using Azure.Core;
using CoSpace.Application.Commands.RefreshTokenCommands;
using CoSpace.Utility.Models.Response;
using CoSpace.API.Services;
using Microsoft.IdentityModel.Tokens;

namespace CoSpace.API.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Authorize]
    public class AdminController(ISender sender, ITokenService tokenService, IMapper mapper, ApiResponse apiResponse) : ControllerBase
    {
        private static HashSet<string> RevokedTokens = new HashSet<string>();

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] AdminLogin request)
        {
            var result = await sender.Send(new AdminLoginQuery(request.Email, request.Password));

            if (result is not null)
            {
                var accessToken = tokenService.GenerateAccessToken(result.Email, result.IsAppAdmin, result.Id, result.OrganizationId, result.RoleId);
                var refreshToken = tokenService.GenerateRefreshToken();

                await tokenService.SaveRefreshToken(result.Id, refreshToken);

                apiResponse.Success = true;
                apiResponse.Data = new
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    Data = result
                };
                return Ok(apiResponse);
            }

            return Unauthorized(new { message = "Invalid username or password." });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {

            Request.Headers.TryGetValue("RefreshToken", out var token);

            var refreshToken = await sender.Send(new GetRefreshTokenQuery(token.ToString()));

            if (refreshToken == null || refreshToken.IsRevoked)
            {
                return BadRequest("Invalid token");
            }

            refreshToken.IsRevoked = true;
            refreshToken.Revoked = DateTime.Now;

            await sender.Send(new DeleteRefreshTokenCommand());

            apiResponse.Success = true;
            apiResponse.Message = "Successfully logged out.";

            return Ok(apiResponse);
        }


        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            if (!Request.Headers.TryGetValue("RefreshToken", out var authorizationHeader))
            {
                return Unauthorized("RefreshToken header missing.");
            }

            var refreshToken = authorizationHeader.ToString();

            try
            {
                var newAccessToken = await tokenService.RefreshAccessToken(refreshToken);
                return Ok(new
                {
                    AccessToken = newAccessToken
                });
            }
            catch (SecurityTokenException)
            {
                return Unauthorized("Invalid or expired refresh token.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAdmins()
        {
            var result = await sender.Send(new GetAdminsQuery());

            var adminDtos = mapper.Map<IEnumerable<AdminDTO>>(result);

            if (adminDtos is not null)
            {

                apiResponse.Success = true;
                apiResponse.Data = adminDtos;

                return Ok(apiResponse);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> AddAdminAsync([FromBody] AdminDTO adminDto)
        {
            var admin = mapper.Map<User>(adminDto);

            var result = await sender.Send(new AddAdminCommand(admin));
            if (result is not null)
            {
                apiResponse.Success = true;
                apiResponse.Data = result;

                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAdminAsync([FromBody] User admin)
        {
            var result = await sender.Send(new UpdateAdminCommand(admin));
            if (result)
            {
                apiResponse.Success = true;
                apiResponse.Data = result;
                return Ok(apiResponse);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin([FromRoute] int id)
        {
            var result = await sender.Send(new DeleteAdminCommand(id));

            if(result)
            {
                apiResponse.Success = true;
                apiResponse.Data = result;
                return Ok(apiResponse);
            }
            return NotFound(apiResponse);
        }
    }
}
