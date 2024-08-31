using CoSpace.Application.Commands;
using CoSpace.Application.Queries;
using CoSpace.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoSpace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController(ISender sender) : ControllerBase
    {
        [HttpPost("/add")]
        public async Task<IActionResult> AddAdminAsync([FromBody] Admin admin)
        {
            var result = await sender.Send(new AddAdminCommand(admin));
            return Ok(result);
        }


        [HttpGet("admins")]
        public async Task<IActionResult> GetAdmins()
        {
            var result = await sender.Send(new GetAdminsQuery());
            return Ok(result);
        }
    }
}
