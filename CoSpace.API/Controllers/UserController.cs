using AutoMapper;
using CoSpace.API.Services.Interface;
using CoSpace.Application.Commands.RefreshTokenCommands;
using CoSpace.Application.Commands.UserCommands;
using CoSpace.Application.Queries.RefreshTokenQueries;
using CoSpace.Application.Queries.UserQueries;
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
    public class UserController(ISender sender, ITokenService tokenService, IMapper mapper, ApiResponse apiResponse, IRefreshTokenService refreshTokenService) : ControllerBase
    {
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLogin request)
        {
            var result = await sender.Send(new UsersLoginQuery(request.Email, request.Password, request.OrgID));

            if (result is not null)
            {
                var accessToken = tokenService.GenerateAccessToken(result.Email, result.Id, result.OrganizationId, result.RoleId);
                var refreshToken = refreshTokenService.GenerateRefreshToken();

                await refreshTokenService.AddRefreshTokenAsync(refreshToken, result.Id, result.OrganizationId);

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
                var newAccessToken = await refreshTokenService.RefreshAccessToken(refreshToken, "user");
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
        public async Task<IActionResult> GetUsers()
        {
            var result = await sender.Send(new GetUsersQuery());

            var UserDtos = mapper.Map<IEnumerable<UserRetrieveDTO>>(result);

            if (UserDtos is not null)
            {

                apiResponse.Success = true;
                apiResponse.Data = UserDtos;

                return Ok(apiResponse);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var result = await sender.Send(new GetUserByIdQuery(id));
            if (result == null)
            {
               apiResponse.Success = false;
                apiResponse.Message = "User not found";
                return NotFound(apiResponse);
            }

            var UserDtos = mapper.Map<UserRetrieveDTO>(result);

            if (UserDtos is not null)
            {

                apiResponse.Success = true;
                apiResponse.Data = UserDtos;

                return Ok(apiResponse);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> AddUserAsync([FromBody] UserCreateDTO UserDto)
        {
            var User = mapper.Map<User>(UserDto);

            var result = await sender.Send(new AddUserCommand(User));
            if (result is not null)
            {
                apiResponse.Success = true;
                apiResponse.Data = result;

                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UserCreateDTO UserDto)
        {
            var User = mapper.Map<User>(UserDto);

            var result = await sender.Send(new UpdateUserCommand(User));
            if (result)
            {
                apiResponse.Success = true;
                apiResponse.Data = result;
                return Ok(apiResponse);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            var result = await sender.Send(new DeleteUserCommand(id));

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
