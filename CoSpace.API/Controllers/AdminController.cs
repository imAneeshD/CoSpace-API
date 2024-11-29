using AutoMapper;
using CoSpace.API.Services.Interface;
using CoSpace.Application.Commands.AdminCommands;
using CoSpace.Application.Commands.RefreshTokenCommands;
using CoSpace.Application.Queries.AdminQueries;
using CoSpace.Application.Queries.RefreshTokenQueries;
using CoSpace.Core.DTO;
using CoSpace.Core.Entities;
using CoSpace.Utility.Models.Request;
using CoSpace.Utility.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CoSpace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdminController(ISender sender, ITokenService tokenService, IMapper mapper, ApiResponse apiResponse, IRefreshTokenService refreshTokenService) : ControllerBase
    {

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] AdminLogin request)
        {
            var result = await sender.Send(new AdminsLoginQuery(request.Email, request.Password));

            if (result is not null)
            {
                var adminDto = mapper.Map<AdminDTO>(result);

                var accessToken = tokenService.GenerateAccessToken(result.Email, result.Id, result.OrganizationId, result.RoleId);
                var refreshToken = refreshTokenService.GenerateRefreshToken();

                await refreshTokenService.AddRefreshTokenAsync(refreshToken, result.Id);

                apiResponse.Success = true;
                apiResponse.Data = new
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    Data = adminDto
                };
                return Ok(apiResponse);
            }

            return Unauthorized(new { message = "Invalid Adminname or password." });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {

            Request.Headers.TryGetValue("RefreshToken", out var token);

            var refreshToken = refreshTokenService.GetRefreshToken(token.ToString()).Result;

            if (refreshToken == null || refreshToken.IsRevoked)
            {
                return BadRequest("Invalid token");
            }

            refreshToken.IsRevoked = true;
            refreshToken.Revoked = DateTime.Now;

            await refreshTokenService.DeleteRefreshToken();

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
                var newAccessToken = await refreshTokenService.RefreshAccessToken(refreshToken, "admin");
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

            var AdminDtos = mapper.Map<IEnumerable<AdminDTO>>(result);

            if (AdminDtos is not null)
            {
                apiResponse.Success = true;
                apiResponse.Data = AdminDtos;

                return Ok(apiResponse);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> AddAdminAsync([FromBody] AdminDTO AdminDto)
        {
            var Admin = mapper.Map<Admin>(AdminDto);

            var result = await sender.Send(new AddAdminCommand(Admin));
            if (result is not null)
            {
                apiResponse.Success = true;
                apiResponse.Data = result;

                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAdminAsync([FromBody] Admin Admin)
        {
            var result = await sender.Send(new UpdateAdminCommand(Admin));
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

            if (result)
            {
                apiResponse.Success = true;
                apiResponse.Data = result;
                return Ok(apiResponse);
            }
            return NotFound(apiResponse);
        }
    }
}
