using CoSpace.Core.Interface;
using CoSpace.Core.Options;
using CoSpace.Infrastruture.Data;
using CoSpace.Infrastruture.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;


namespace CoSpace.Infrastruture
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>((provider, options) =>
            {
                options.UseSqlServer(provider.GetRequiredService<IOptionsSnapshot<ConnectionStringOptions>>().Value.Local);
            });
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            return services;
        }
    }
}
