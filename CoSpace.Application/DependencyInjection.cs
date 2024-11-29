using CoSpace.Application.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace CoSpace.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {

            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }
    }
}
