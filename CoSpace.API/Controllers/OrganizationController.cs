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

        public async Task<IActionResult> AddOrganization([FromBody] Organization organization)
        {
            var result = await sender.Send(new AddOrganizationCommand(organization));
            if(result is not null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
