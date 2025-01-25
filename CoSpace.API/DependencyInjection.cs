using CoSpace.API.Services;
using CoSpace.API.Services.Interface;
using CoSpace.Application;
using CoSpace.Core;
using CoSpace.Infrastructure.Services;
using CoSpace.Infrastruture;
using CoSpace.Infrastruture.Services.Interface;
using CoSpace.Utility.Models.Response;

namespace CoSpace.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationDI()
                .AddInfrastructureDI()
                .AddCoreDI(configuration);
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IRoomStatusService, RoomStatusService>();
            services.AddTransient<ApiResponse>();
            return services;
        }
    }
}
