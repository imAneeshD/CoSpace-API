using CoSpace.Application.Commands.OrganizationCommands;
using CoSpace.Application.Queries.OrganizationQueries;
using CoSpace.Core.Entities;
using CoSpace.Utility.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoSpace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrganizationController(ISender sender, ApiResponse apiResponse) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> AddOrganization([FromBody] Organization organization)
        {
            try
            {
                if (organization == null)
                {
                    apiResponse.Success = false;
                    apiResponse.Message = "Organization data is required.";
                    return BadRequest(apiResponse);
                }

                var result = await sender.Send(new AddOrganizationCommand(organization));

                if (result != null)
                {
                    apiResponse.Data = result;
                    return CreatedAtAction(nameof(AddOrganization), new { id = result.Id }, apiResponse);
                }

                apiResponse.Success = false;
                apiResponse.Message = "Something went wrong.";
                return StatusCode(StatusCodes.Status400BadRequest, apiResponse);
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = "An error occurred while processing your request.";
                apiResponse.Data = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, apiResponse);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrganization([FromBody] Organization organization)
        {
            try
            {
                if (organization == null)
                {
                    apiResponse.Success = false;
                    apiResponse.Message = "Organization data is required.";
                    return BadRequest(apiResponse);
                }

                var result = await sender.Send(new UpdateOrganizationCommand(organization));
                if (result)
                {
                    apiResponse.Data = result;
                    return Ok(apiResponse);
                }
                apiResponse.Success = false;
                apiResponse.Message = "Something went wrong.";
                return StatusCode(StatusCodes.Status400BadRequest, apiResponse);
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = "An error occurred while processing your request.";
                apiResponse.Data = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, apiResponse);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteOrganization([FromRoute] int id)
        {
            try
            {
                var result = await sender.Send(new DeleteOrganizationCommand(id));

                if (result)
                {
                    apiResponse.Data = result;
                    return Ok(apiResponse);
                }
                apiResponse.Success = false;
                apiResponse.Message = "Not Found.";
                return StatusCode(404, apiResponse);
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = "An error occurred while processing your request.";
                apiResponse.Data = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, apiResponse);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrganization()
        {
            try
            {
                var result = await sender.Send(new GetAllOrganizationsQuery());

                if (result.Any())
                {
                    apiResponse.Data = result;
                    return Ok(apiResponse);
                }
                apiResponse.Success = false;
                apiResponse.Message = "No data found.";
                return StatusCode(404, apiResponse);
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = "An error occurred while processing your request.";
                apiResponse.Data = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, apiResponse);
            }

        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOrganizationByID([FromRoute] int id)
        {
            try
            {
                var result = await sender.Send(new GetOrganizationByIdQuery(id));

                if (result is not null)
                {
                    apiResponse.Data = result;
                    return Ok(apiResponse);
                }
                apiResponse.Success = false;
                apiResponse.Message = "Not Found.";
                return StatusCode(404, apiResponse);
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.Message = "An error occurred while processing your request.";
                apiResponse.Data = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, apiResponse);
            }
        }
    }
}
