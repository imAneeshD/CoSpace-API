using CoSpace.Application.Commands.CanteenCommands;
using CoSpace.Application.Queries.CanteenMenuQueries;
using CoSpace.Core.Entities;
using CoSpace.Utility.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoSpace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CanteenMenuController(ISender sender, ApiResponse apiResponse) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddCanteenMenuMenu([FromBody] CanteenMenu CanteenMenu)
        {
            try
            {
                if (CanteenMenu == null)
                {
                    apiResponse.Success = false;
                    apiResponse.Message = "CanteenMenu data is required.";
                    return BadRequest(apiResponse);
                }
                var result = await sender.Send(new AddCanteenMenuCommand(CanteenMenu));
                if (result != null)
                {
                    apiResponse.Data = result;
                    return CreatedAtAction(nameof(AddCanteenMenuMenu), new { id = result.Id }, apiResponse);
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
        public async Task<IActionResult> UpdateCanteenMenu([FromBody] CanteenMenu CanteenMenu)
        {
            try
            {
                if (CanteenMenu == null)
                {
                    apiResponse.Success = false;
                    apiResponse.Message = "CanteenMenu data is required.";
                    return BadRequest(apiResponse);
                }
                var result = await sender.Send(new UpdateCanteenMenuCommand(CanteenMenu));
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
        public async Task<IActionResult> DeleteCanteenMenu(int id)
        {
            try
            {
                var canteenMenu = await sender.Send(new GetCanteenMenuByIdQuery(id));
                if (canteenMenu == null)
                {
                    apiResponse.Success = false;
                    apiResponse.Message = "CanteenMenu not found";
                    return NotFound(apiResponse);
                }

                var result = await sender.Send(new DeleteCanteenMenuCommand(canteenMenu));
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCanteenMenu(int id)
        {
            try
            {
                var result = await sender.Send(new GetCanteenMenuByIdQuery(id));
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

        [HttpGet]
        public async Task<IActionResult> GetCanteenMenus()
        {
            try
            {
                var result = await sender.Send(new GetAllCanteenMenuQuery());
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