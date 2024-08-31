using CoSpace.Application;
using CoSpace.Infrastruture;

namespace CoSpace.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppDI(this IServiceCollection services)
        {
            services.AddApplicationDI()
                .AddInfrastructureDI() ;

            return services;
        }
    }
}
