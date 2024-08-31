using CoSpace.API.Models.Request;
using CoSpace.API.Services.Interface;
using CoSpace.Application.Commands;
using CoSpace.Application.Queries;
using CoSpace.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoSpace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController(ISender sender, ITokenService tokenService) : ControllerBase
    {

        [Authorize]
        [HttpGet("admins")]
        public async Task<IActionResult> GetAdmins()
        {
            var result = await sender.Send(new GetAdminsQuery());
            return Ok(result);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] AdminLogin request)
        {
            // Authenticate user (replace this with actual authentication logic)
            if (request.Email == "admin@gmail.com" && request.Password == "admin")
            {
                var token = tokenService.GenerateToken(request.Email);
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddAdminAsync([FromBody] Admin admin)
        {
            var result = await sender.Send(new AddAdminCommand(admin));
            return Ok(result);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateAdminAsync([FromRoute] int id, [FromBody] Admin admin)
        {
            var result = await sender.Send(new UpdateAdminCommand(id, admin));
            return Ok(result);
        }


    }
}
