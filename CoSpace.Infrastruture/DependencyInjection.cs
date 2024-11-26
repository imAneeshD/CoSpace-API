using CoSpace.Core.Interface;
using CoSpace.Core.Options;
using CoSpace.Infrastructure.Services;
using CoSpace.Infrastruture.Data;
using CoSpace.Infrastruture.Repository;
using CoSpace.Infrastruture.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;


namespace CoSpace.Infrastruture
{

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services)
        {
            // Register DbContext
            services.AddDbContext<ApplicationDbContext>((provider, options) =>
            {
                options.UseSqlServer(provider.GetRequiredService<IOptionsSnapshot<ConnectionStringOptions>>().Value.Local);
            });

            // Register repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            services.AddScoped(typeof(RepositoryBase<>), typeof(RepositoryBase<>));

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            return services;
        }
    }

}
