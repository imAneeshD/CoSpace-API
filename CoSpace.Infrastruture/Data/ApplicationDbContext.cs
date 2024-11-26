using CoSpace.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoSpace.Infrastruture.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Organization> Organization { get; set; }
        public DbSet<UserRole> Role { get; set; }
        public DbSet<UserRole> UserType { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }
        public DbSet<UserRole> OrganizationUserType { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Organization>().HasData(
            new Organization
            {
                Id = 1,
                Name = "CoSpace Org",
                PrimaryEmail = "aneeshd70+cospace@gmail.com",
                SecondaryEmail = "aneeshd70+cospace2@gmail.com",
                Phone = "987654321",
                Domain = "cospace.com",
                Location = "",
                CreatedBy = 0,
                CreatedDate = DateTime.Now,
                UpdatedBy = 0,
                UpdatedDate = DateTime.Now,
                IsDeleted = false,
            }
        );

            modelBuilder.Entity<UserRole>().HasData(
      new UserRole
      {
          Id = 1,
          OrganizationId = 1,
          Name = "cospace_admin",
          CreatedBy = 0,
          CreatedDate = DateTime.Now,
          UpdatedBy = 0,
          UpdatedDate = DateTime.Now,
          IsDeleted = false,
      }
  );
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
                    RoleId = 1,
                    OrganizationId = 1,
                    IsAppAdmin = true,
                }
            );
        }
    }
}
