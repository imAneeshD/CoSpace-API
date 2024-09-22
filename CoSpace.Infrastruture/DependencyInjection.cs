using CoSpace.Core.Interface;
using CoSpace.Infrastruture.Data;
using CoSpace.Infrastruture.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSpace.Infrastruture
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer("Server=SHADOW\\SQLEXPRESS;Database=CoSpace;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True");
                //options.UseSqlServer("Server=PWSMLRPW364\\SQLEXPRESS;Database=CoSpace;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True");
            });
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();

            return services;
        }
    }
}
