using CoSpace.Infrastruture.Services;
using CoSpace.Infrastruture.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class AdminOnlyAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // Allow anonymous access if `[AllowAnonymous]` is applied
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata
                               .OfType<AllowAnonymousAttribute>()
                               .Any();

        if (allowAnonymous)
        {
            return; // Skip authorization
        }

        // Fetch the current admin service
        var currentAdminService = context.HttpContext.RequestServices.GetService<ICurrentUserService>();

        if (currentAdminService == null ||
            (currentAdminService.OrgId != 1 && currentAdminService.Role != 1 && currentAdminService.Role != 2))
        {
            context.Result = new UnauthorizedResult();
        }
    }
}
