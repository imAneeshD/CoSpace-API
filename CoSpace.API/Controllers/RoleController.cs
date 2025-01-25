using CoSpace.Application.Commands.RefreshTokenCommands;
using CoSpace.Application.Commands.UserRoleCommands;
using CoSpace.Application.Queries.UserQueries;
using CoSpace.Application.Queries.UserRoleQueries;
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
    [AdminOnly]
    public class RoleController(ISender sender, ApiResponse apiResponse) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddRole([FromBody] UserRole role)
        {
            try
            {
                if (role == null)
                {
                    apiResponse.Success = false;
                    apiResponse.Message = "Role data is required.";
                    return BadRequest(apiResponse);
                }
                var result = await sender.Send(new AddUserRoleCommand(role));
                if (result != null)
                {
                    apiResponse.Data = result;
                    return CreatedAtAction(nameof(AddRole), new { id = result.Id }, apiResponse);
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
        public async Task<IActionResult> UpdateRole([FromBody] UserRole role)
        {
            try
            {
                if (role == null)
                {
                    apiResponse.Success = false;
                    apiResponse.Message = "Role data is required.";
                    return BadRequest(apiResponse);
                }
                var result = await sender.Send(new UpdateUserRoleCommand(role));
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole([FromRoute] int id)
        {
            try
            {
                var role = await sender.Send(new GetUserRoleByIdQuery(id));
                if (role == null)
                {
                    apiResponse.Success = false;
                    apiResponse.Message = "Role not found.";
                    return StatusCode(StatusCodes.Status404NotFound, apiResponse);
                }
                var result = await sender.Send(new DeleteUserRoleCommand(role));
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRole([FromRoute] int id)
        {
            try
            {
                var result = await sender.Send(new GetUserRoleByIdQuery(id));
                if (result != null)
                {
                    apiResponse.Data = result;
                    return Ok(apiResponse);
                }
                apiResponse.Success = false;
                apiResponse.Message = "Role not found.";
                return StatusCode(StatusCodes.Status404NotFound, apiResponse);
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
        public async Task<IActionResult> GetAllRoles()
        {
            try
            {
                var result = await sender.Send(new GetAllUserRolesQuery());
                if (result.Any())
                {
                    apiResponse.Data = result;
                    return Ok(apiResponse);
                }
                apiResponse.Success = false;
                apiResponse.Message = "No data found.";
                return StatusCode(StatusCodes.Status404NotFound, apiResponse);
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
