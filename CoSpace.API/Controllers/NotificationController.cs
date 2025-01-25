using CoSpace.Application.Commands.NotificationCommands;
using CoSpace.Application.Queries.NotificationQueries;
using CoSpace.Core.Entities;
using CoSpace.Utility.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoSpace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController(ISender sender, ApiResponse apiResponse) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddNotification([FromBody] Notification notification)
        {
            try
            {
                if (notification == null)
                {
                    apiResponse.Success = false;
                    apiResponse.Message = "Notification data is required.";
                    return BadRequest(apiResponse);
                }
                var result = await sender.Send(new AddNotificationCommand(notification));
                if (result != null)
                {
                    apiResponse.Data = result;
                    return CreatedAtAction(nameof(AddNotification), new { id = result.Id }, apiResponse);
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
        public async Task<IActionResult> UpdateNotification([FromBody] Notification notification)
        {
            try
            {
                if (notification == null)
                {
                    apiResponse.Success = false;
                    apiResponse.Message = "Notification data is required.";
                    return BadRequest(apiResponse);
                }
                var result = await sender.Send(new UpdateNotificationCommand(notification));
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
        public async Task<IActionResult> DeleteNotification(int id)
        {
            try
            {
                if (id <= 0)
                {
                    apiResponse.Success = false;
                    apiResponse.Message = "Notification id is required.";
                    return BadRequest(apiResponse);
                }
                var result = await sender.Send(new DeleteNotificationCommand(id));
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
        public async Task<IActionResult> GetNotifications()
        {
            try
            {
                var result = await sender.Send(new GetAllNotificationQuery());
                if (result != null)
                {
                    apiResponse.Data = result;
                    return Ok(apiResponse);
                }
                apiResponse.Success = false;
                apiResponse.Message = "No notification found.";
                return NotFound(apiResponse);
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
        public async Task<IActionResult> GetNotification(int id)
        {
            try
            {
                if (id <= 0)
                {
                    apiResponse.Success = false;
                    apiResponse.Message = "Notification id is required.";
                    return BadRequest(apiResponse);
                }
                var result = await sender.Send(new GetNotificationByIdQuery(id));
                if (result != null)
                {
                    apiResponse.Data = result;
                    return Ok(apiResponse);
                }
                apiResponse.Success = false;
                apiResponse.Message = "Notification not found.";
                return NotFound(apiResponse);
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
