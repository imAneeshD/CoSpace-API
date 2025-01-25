using AutoMapper;
using CoSpace.API.Services.Interface;
using CoSpace.Application.Commands.BookingCommands;
using CoSpace.Application.Commands.RoomCommands;
using CoSpace.Application.Queries.BookingQueries;
using CoSpace.Application.Queries.RoomQueries;
using CoSpace.Core.Entities;
using CoSpace.Utility.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoSpace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController(ISender sender, IRoomStatusService roomStatusService, IMapper mapper, ApiResponse apiResponse, IRefreshTokenService refreshTokenService) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> AddBooking([FromBody] Booking bookingRequest)
        {
            try
            {

                // Update room status before proceeding
                await roomStatusService.UpdateRoomStatusAsync(bookingRequest.RoomId);

                var room = await sender.Send(new GetRoomByIdQuery(bookingRequest.RoomId));
                var bookings = await sender.Send(new GetAllBookingQuery());

                // Ensure the room is available
                if (room.Status != "Available")
                {
                    apiResponse.Success = false;
                    apiResponse.Message = $"Room is currently {room.Status}";
                    return BadRequest(apiResponse);
                }

                // Check for overlapping bookings
                bool isRoomBooked = bookings.Any(booking =>
                    booking.RoomId == bookingRequest.RoomId &&
                    booking.Status.ToLower() == "confirmed" &&
                    booking.StartTime < bookingRequest.EndTime &&
                    booking.EndTime > bookingRequest.StartTime);

                if (isRoomBooked)
                {
                    apiResponse.Success = false;
                    apiResponse.Message = "Room is already booked for the selected time.";
                    return BadRequest(apiResponse);
                }

                // Proceed with booking
                var result = await sender.Send(new AddBookingCommand(bookingRequest));

                if (result != null)
                {
                    apiResponse.Data = result;
                    room.Status = "Booked";
                    await sender.Send(new UpdateRoomCommand(room));

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
                apiResponse.Data = ex.InnerException?.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, apiResponse);
            }
        }



        [HttpPut]
        public async Task<IActionResult> UpdateBooking([FromBody] Booking bookingRequest, [FromServices] IRoomStatusService roomStatusService)
        {
            try
            {

                // First, check and update the room status (if necessary)
                await roomStatusService.UpdateRoomStatusAsync(bookingRequest.RoomId);

                var room = await sender.Send(new GetRoomByIdQuery(bookingRequest.RoomId));

                // If the room is already booked and the status is being updated to 'confirmed', return an error
                if (room.Status.ToLower() == "booked" && bookingRequest.Status.ToLower() == "confirmed")
                {
                    apiResponse.Success = false;
                    apiResponse.Message = "Room is already booked.";
                    return BadRequest(apiResponse);
                }

                // You can add additional logic here if necessary, such as validating the updated booking details
                var result = await sender.Send(new UpdateBookingCommand(bookingRequest));
                if (result)
                {
                    apiResponse.Success = true;
                    apiResponse.Message = "Booking updated successfully.";
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

                var booking = await sender.Send(new GetBookingByIdQuery(id));
                if (booking == null)
                {
                    apiResponse.Success = false;
                    apiResponse.Message = "Booking not found.";
                    return NotFound(apiResponse);
                }
                var result = await sender.Send(new DeleteBookingCommand(booking));
                if (result)
                {
                    var room = await sender.Send(new GetRoomByIdQuery(booking.RoomId));
                    room.Status = "Available";
                    await sender.Send(new UpdateRoomCommand(room));
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            try
            {
                if (id == 0)
                {
                    apiResponse.Success = false;
                    apiResponse.Message = "Booking id is required.";
                    return BadRequest(apiResponse);
                }
                var result = await sender.Send(new GetBookingByIdQuery(id));
                if (result != null)
                {
                    apiResponse.Data = result;
                    return Ok(apiResponse);
                }
                apiResponse.Message = "Not found.";
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
