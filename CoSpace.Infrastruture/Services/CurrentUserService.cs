using CoSpace.Infrastruture.Services.Interface;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CoSpace.Infrastructure.Services
{
    public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
    {
        public int UserId
        {
            get
            {
                var userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirst("Id")?.Value;
                return userIdClaim != null ? int.Parse(userIdClaim) : 0;
            }
        }

        public int OrgId
        {
            get
            {
                var userTypeClaim = httpContextAccessor.HttpContext?.User?.FindFirst("OrgId")?.Value;
                return userTypeClaim != null ? int.Parse(userTypeClaim) : 0;
            }
        }

        public int Role
        {
            get
            {
                var userTypeClaim = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;
                return userTypeClaim != null ? int.Parse(userTypeClaim) : 0;
            }
        }

        public int AppAdmin
        {
            get
            {
                var userTypeClaim = httpContextAccessor.HttpContext?.User?.FindFirst("AppUserTypeId")?.Value;
                return userTypeClaim != null ? int.Parse(userTypeClaim) : 0;
            }
        }
    }
}
