using CoSpace.API.Services.Interface;
using CoSpace.Application.Commands.OrganizationCommands;
using CoSpace.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoSpace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrganizationController(ISender sender) : ControllerBase
    {
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddOrganization([FromBody] Organization organization)
        {
            var result = await sender.Send(new AddOrganizationCommand(organization));
            if(result is not null)
            {
                return Created();
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateOrganization([FromBody] Organization organization)
        {
            var result = await sender.Send(new UpdateOrganizationCommand(organization));
            if (result)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
