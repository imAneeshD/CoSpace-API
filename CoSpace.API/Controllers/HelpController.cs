using CoSpace.Application.Commands.HelpCommands;
using CoSpace.Application.Queries.HelpQueries;
using CoSpace.Core.Entities;
using CoSpace.Utility.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoSpace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelpController(ISender sender, ApiResponse apiResponse) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddHelp([FromBody] HelpRequest help)
        {
            try
            {
                if (help == null)
                {
                    apiResponse.Success = false;
                    apiResponse.Message = "Help data is required.";
                    return BadRequest(apiResponse);
                }
                var result = await sender.Send(new AddHelpRequestCommand(help));
                if (result != null)
                {
                    apiResponse.Data = result;
                    return CreatedAtAction(nameof(AddHelp), new { id = result.Id }, apiResponse);
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
        public async Task<IActionResult> UpdateHelp([FromBody] HelpRequest help)
        {
            try
            {
                if (help == null)
                {
                    apiResponse.Success = false;
                    apiResponse.Message = "Help data is required.";
                    return BadRequest(apiResponse);
                }
                var result = await sender.Send(new UpdateHelpRequestCommand(help));
                if (result)
                {
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
        public async Task<IActionResult> DeleteHelp(int id)
        {
            try
            {
                var result = await sender.Send(new DeleteHelpRequestCommand(id));
                if (result)
                {
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

        [HttpGet]
        public async Task<IActionResult> GetHelps()
        {
            try
            {
                var result = await sender.Send(new GetAllHelpRequestQuery());
                if (result != null)
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
        public async Task<IActionResult> GetHelp(int id)
        {
            try
            {
                var result = await sender.Send(new GetHelpRequestByIdQuery(id));
                if (result != null)
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
    }
}
