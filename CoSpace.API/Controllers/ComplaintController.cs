using CoSpace.Application.Commands.ComplaintCommands;
using CoSpace.Application.Queries.ComplaintQueries;
using CoSpace.Core.Entities;
using CoSpace.Utility.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoSpace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintController(ISender sender, ApiResponse apiResponse) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddComplaint([FromBody] Complaint complaint)
        {
            try
            {
                if (complaint == null)
                {
                    apiResponse.Success = false;
                    apiResponse.Message = "Complaint data is required.";
                    return BadRequest(apiResponse);
                }
                var result = await sender.Send(new AddComplaintCommand(complaint));
                if (result != null)
                {
                    apiResponse.Data = result;
                    return CreatedAtAction(nameof(AddComplaint), new { id = result.Id }, apiResponse);
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
        public async Task<IActionResult> UpdateComplaint([FromBody] Complaint complaint)
        {
            try
            {
                if (complaint == null)
                {
                    apiResponse.Success = false;
                    apiResponse.Message = "Complaint data is required.";
                    return BadRequest(apiResponse);
                }
                var result = await sender.Send(new UpdateComplaintCommand(complaint));
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
        public async Task<IActionResult> DeleteComplaint(int id)
        {
            try
            {
                if (id == 0)
                {
                    apiResponse.Success = false;
                    apiResponse.Message = "Complaint id is required.";
                    return BadRequest(apiResponse);
                }
                var result = await sender.Send(new DeleteComplaintCommand(id));
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
        public async Task<IActionResult> GetComplaint(int id)
        {
            try
            {
                var result = await sender.Send(new GetComplaintByIdQuery(id));
                if (result != null)
                {
                    apiResponse.Data = result;
                    return Ok(apiResponse);
                }
                apiResponse.Success = false;
                apiResponse.Message = "Complaint not found.";
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

        [HttpGet]
        public async Task<IActionResult> GetComplaints()
        {
            try
            {
                var result = await sender.Send(new GetAllComplaintQuery());
                if (result != null)
                {
                    apiResponse.Data = result;
                    return Ok(apiResponse);
                }
                apiResponse.Success = false;
                apiResponse.Message = "No complaints found.";
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
