using AutoMapper;
using CoSpace.API.Services.Interface;
using CoSpace.Application.Commands.BookingCommands;
using CoSpace.Application.Queries.BookingQueries;
using CoSpace.Core.Entities;
using CoSpace.Utility.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoSpace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController(ISender sender, IHttpContextAccessor httpContextAccessor, ITokenService tokenService, IMapper mapper, ApiResponse apiResponse, IRefreshTokenService refreshTokenService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddBooking([FromBody] Booking bookingRequest)
        {
            try
            {
                if (bookingRequest == null)
                {
                    apiResponse.Success = false;
                    apiResponse.Message = "Booking data is required.";
                    return BadRequest(apiResponse);
                }
                var result = await sender.Send(new AddBookingCommand(bookingRequest));
                if (result != null)
                {
                    apiResponse.Data = result;
                    return CreatedAtAction(nameof(AddBooking), new { id = result.Id }, apiResponse);
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
        public async Task<IActionResult> UpdateBooking([FromBody] Booking bookingRequest)
        {
            try
            {
                if (bookingRequest == null)
                {
                    apiResponse.Success = false;
                    apiResponse.Message = "Booking data is required.";
                    return BadRequest(apiResponse);
                }
                var result = await sender.Send(new UpdateBookingCommand(bookingRequest));
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
        public async Task<IActionResult> DeleteBooking(int id)
        {
            try
            {
                if (id == 0)
                {
                    apiResponse.Success = false;
                    apiResponse.Message = "Booking id is required.";
                    return BadRequest(apiResponse);
                }
                var result = await sender.Send(new DeleteBookingCommand(id));
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
        public async Task<IActionResult> GetBookings()
        {
            try
            {
                var result = await sender.Send(new GetAllBookingQuery());
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
