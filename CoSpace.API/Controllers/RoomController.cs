using CoSpace.Application.Commands.RoomCommands;
using CoSpace.Application.Queries.RoomQueries;
using CoSpace.Core.Entities;
using CoSpace.Utility.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoSpace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController(ISender sender, ApiResponse apiResponse) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddRoom([FromBody] Room room)
        {
            try
            {
                if (room == null)
                {
                    apiResponse.Success = false;
                    apiResponse.Message = "Room data is required.";
                    return BadRequest(apiResponse);
                }
                var result = await sender.Send(new AddRoomCommand(room));
                if (result != null)
                {
                    apiResponse.Data = result;
                    return CreatedAtAction(nameof(AddRoom), new { id = result.Id }, apiResponse);
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
        public async Task<IActionResult> UpdateRoom([FromBody] Room room)
        {
            try
            {
                if (room == null)
                {
                    apiResponse.Success = false;
                    apiResponse.Message = "Room data is required.";
                    return BadRequest(apiResponse);
                }
                var result = await sender.Send(new UpdateRoomCommand(room));
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
        public async Task<IActionResult> DeleteRoom(int id)
        {
            try
            {
                if (id <= 0)
                {
                    apiResponse.Success = false;
                    apiResponse.Message = "Room id is required.";
                    return BadRequest(apiResponse);
                }
                var result = await sender.Send(new DeleteRoomCommand(id));
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
        public async Task<IActionResult> GetRooms()
        {
            try
            {
                var result = await sender.Send(new GetAllRoomQuery());
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
        public async Task<IActionResult> GetRoom(int id)
        {
            try
            {
                if (id <= 0)
                {
                    apiResponse.Success = false;
                    apiResponse.Message = "Room id is required.";
                    return BadRequest(apiResponse);
                }
                var result = await sender.Send(new GetRoomByIdQuery(id));
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
