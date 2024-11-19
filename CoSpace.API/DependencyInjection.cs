using CoSpace.Application;
using CoSpace.Core;
using CoSpace.Infrastruture;
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

            services.AddTransient<ApiResponse>();
            return services;
        }
    }
}
