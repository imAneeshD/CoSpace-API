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

namespace CoSpace.API.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Authorize]
    public class AdminController(ISender sender, ITokenService tokenService) : ControllerBase
    {

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] AdminLogin request)
        {
            var result = await sender.Send(new AdminLoginQuery(request.Email, request.Password));

            if (result is not null)
            {
                var token = tokenService.GenerateToken(request.Email, "admin");
                return Ok(new { Token = token });
            }

            return Unauthorized(new { message = "Invalid username or password." });
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAdmins()
        {
            var result = await sender.Send(new GetAdminsQuery());
            if (result is not null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddAdminAsync([FromBody] Admin admin)
        {
            var result = await sender.Send(new AddAdminCommand(admin));
            if (result is not null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateAdminAsync([FromRoute] int id, [FromBody] Admin admin)
        {
            var result = await sender.Send(new UpdateAdminCommand(id, admin));
            if (result)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> GetAdminById([FromRoute] int id)
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
