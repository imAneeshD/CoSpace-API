using AutoMapper;
using CoSpace.API.Services.Interface;
using CoSpace.Application.Commands.AdminCommands;
using CoSpace.Application.Queries.AdminQueries;
using CoSpace.Core.DTO;
using CoSpace.Core.Entities;
using CoSpace.Utility.Models.Request;
using CoSpace.Utility.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CoSpace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [AdminOnly]
    public class AdminController(ISender sender, IHttpContextAccessor httpContextAccessor, ITokenService tokenService, IMapper mapper, ApiResponse apiResponse, IRefreshTokenService refreshTokenService) : ControllerBase
    {

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] AdminLogin request)
        {
            var result = await sender.Send(new AdminsLoginQuery(request.Email, request.Password));

            if (result is not null)
            {
                var adminDto = mapper.Map<AdminDTO>(result);

                var accessToken = tokenService.GenerateAccessToken(result.Email, result.Id, result.OrganizationId, result.RoleId);
                var refreshToken = refreshTokenService.GenerateRefreshToken();

                await refreshTokenService.AddRefreshTokenAsync(refreshToken, result.Id, result.OrganizationId);

                apiResponse.Data = new
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    Data = adminDto
                };
                return Ok(apiResponse);
            }

            return Unauthorized(new { message = "Invalid credentials." });
        }

        [AllowAnonymous]
        [HttpDelete("logout")]
        public async Task<IActionResult> Logout()
        {
            Request.Headers.TryGetValue("RefreshToken", out var token);

            var refreshToken = refreshTokenService.GetRefreshToken(token.ToString()).Result;
            if (refreshToken == null || refreshToken.IsRevoked)
            {
                return BadRequest("Invalid token");
            }

            refreshToken.IsRevoked = true;
            refreshToken.Revoked = DateTime.Now;

            await refreshTokenService.DeleteRefreshToken();

            apiResponse.Message = "Successfully logged out.";

            return Ok(apiResponse);
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            if (!Request.Headers.TryGetValue("RefreshToken", out var token))
            {
                return BadRequest("RefreshToken header missing.");
            }

            var refreshToken = token.ToString();
            if (refreshToken == string.Empty)
            {
                return BadRequest("RefreshToken is empty");
            }

            try
            {
                var newAccessToken = await refreshTokenService.RefreshAccessToken(refreshToken, "admin");

                var tokenResponse = new
                {
                    AccessToken = newAccessToken,
                    RefreshToken = refreshToken
                };

                var apiResponse = new ApiResponse
                {
                    Data = tokenResponse // Send both tokens in the response data
                };
                return Ok(apiResponse);
            }
            catch (SecurityTokenException)
            {
                return StatusCode(410, "Invalid or expired refresh token.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAdmins()
        {

            var result = await sender.Send(new GetAdminsQuery());

            var AdminDtos = mapper.Map<IEnumerable<AdminDTO>>(result);

            if (AdminDtos is not null)
            {
                apiResponse.Data = AdminDtos;

                return Ok(apiResponse);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdminById([FromRoute] int id)
        {
            var result = await sender.Send(new GetAdminByIdQuery(id));
            var AdminDto = mapper.Map<AdminDTO>(result);
            if (AdminDto is not null)
            {
                apiResponse.Data = AdminDto;
                return Ok(apiResponse);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddAdminAsync([FromBody] Admin admin)
        {
            var result = await sender.Send(new AddAdminCommand(admin));
            var adminDTO = admin != null ? mapper.Map<AdminDTO>(admin) : null;
            if (result is not null)
            {
                apiResponse.Data = adminDTO;

                return Ok(apiResponse);
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAdminAsync([FromBody] Admin Admin)
        {
            var result = await sender.Send(new UpdateAdminCommand(Admin));
            if (result)
            {
                apiResponse.Data = result;
                return Ok(apiResponse);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin([FromRoute] int id)
        {
            var result = await sender.Send(new DeleteAdminCommand(id));

            if (result)
            {
                apiResponse.Data = result;
                return Ok(apiResponse);
            }
            return NotFound(apiResponse);
        }

        [HttpGet("stats")]
        public IActionResult GetAdminDashboardStats()
        {
            var data = new
            {
                TotalAdmins = 10, // Replace with actual DB query
                TotalOrganizations = 25,
                ActiveBookings = 120,
                OpenTickets = 15,
                BookingTrends = new int[] { 50, 60, 70, 80, 100 }, // Example dataset
                RoomUtilization = new int[] { 30, 40, 20, 50, 70 },
                RecentActivities = new[]
                {
                new { Title = "New Organization Registered", Description = "TechCorp joined.", Time = "2 hours ago", Icon = "fa-building" },
                new { Title = "Booking Created", Description = "Conference Room A booked.", Time = "5 hours ago", Icon = "fa-calendar-check" }
            },
                KeyMetrics = new
                {
                    AverageBookingDuration = "2.5 hours",
                    PeakBookingTime = "10:00 AM - 12:00 PM",
                    MostBookedRoom = "Conference Room A",
                    TicketResolutionTime = "24 hours"
                }
            };
            apiResponse.Data = data;
            return Ok(apiResponse);
        }
    }
}
