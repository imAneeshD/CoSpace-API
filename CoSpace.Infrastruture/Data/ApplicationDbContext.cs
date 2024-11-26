using CoSpace.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoSpace.Infrastruture.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Organization> Organization { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<OrganizationUserType> UserType { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }
        public DbSet<AppUserType> AppUserType { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // Seed default Admin
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Password = "admin123",
                    Email = "admin@gmail.com",
                    FirstName = "Default",
                    LastName = "Admin",
                    AppUserTypeId = 1,
                    RoleId = 0 ,
                    OrganizationId = 0 
                }
            );

            modelBuilder.Entity<AppUserType>().HasData(
                new AppUserType
                {
                    Id = 1,
                    Name = "admin",
                    CreatedBy = 0,
                    CreatedDate = DateTime.Now,
                    UpdatedBy = 0,
                    UpdatedDate = DateTime.Now,
                    IsDeleted = false,
                }, new AppUserType
                {
                    Id = 2,
                    Name = "user",
                    CreatedBy = 0,
                    CreatedDate = DateTime.Now,
                    UpdatedBy = 0,
                    UpdatedDate = DateTime.Now,
                    IsDeleted = false,
                }

            );
        }
    }
}
