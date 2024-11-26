using Azure.Core;
using CoSpace.Application.Queries.RefreshTokenQueries;
using CoSpace.Infrastruture.Data;
using MediatR;

namespace CoSpace.API.Services
{
    public class TokenRevocationMiddleware(ISender sender, RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
           
        }
    }

}
