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

namespace CoSpace.API.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Authorize]
    public class AdminController(IMemoryCache cache, ISender sender, ITokenService tokenService, IMapper mapper) : ControllerBase
    {
        private static HashSet<string> RevokedTokens = new HashSet<string>();

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] AdminLogin request)
        {
            var result = await sender.Send(new AdminLoginQuery(request.Email, request.Password));

            if (result is not null)
            {
                var token = tokenService.GenerateAccessToken(result.Email, "admin", result.Id, 0);
                
                return Ok(new { Token = token , Data = result});
            }

            return Unauthorized(new { message = "Invalid username or password." });
        }

        [HttpPost("logout")]
        public IActionResult Logout([FromBody] LogoutRequest request)
        {
            var refreshToken = await sender.Send(new GetRefreshTokenQuery(admin));

            if (refreshToken == null || refreshToken.IsRevoked)
            {
                return BadRequest("Invalid token");
            }

            refreshToken.IsRevoked = true;
            refreshToken.Revoked = DateTime.UtcNow;

            _context.SaveChanges();

            return Ok(new { message = "Successfully logged out" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAdmins()
        {
            var result = await sender.Send(new GetAdminsQuery());

            var adminDtos = mapper.Map<IEnumerable<AdminDTO>>(result);

            if (adminDtos is not null)
            {
                return Ok(adminDtos);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> AddAdminAsync([FromBody] AdminDTO adminDto)
        {
            var admin = mapper.Map<Admin>(adminDto);

            var result = await sender.Send(new AddAdminCommand(admin));
            if (result is not null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAdminAsync([FromBody] Admin admin)
        {
            var result = await sender.Send(new UpdateAdminCommand(admin));
            if (result)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin([FromRoute] int id)
        {
            var result = await sender.Send(new DeleteAdminCommand(id));

            if(result)
            {
                return Ok(result);
            }

            return NotFound(new { message = $"Admin with ID {id} not found." });

        }
    }
}
