using System.Security.Claims;
using CoSpace.Core.Interface;
using CoSpace.Infrastruture.Services.Interface;
using Microsoft.AspNetCore.Http;

namespace CoSpace.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int UserId
        {
            get
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("Id")?.Value;
                return userIdClaim != null ? int.Parse(userIdClaim) : 0;
            }
        }

        public int UserType
        {
            get
            {
                var userTypeClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;
                return userTypeClaim != null ? int.Parse(userTypeClaim) : 0;
            }
        }
    }
}
