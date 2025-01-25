using CoSpace.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoSpace.Infrastruture.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Organization> Organization { get; set; }
        public DbSet<UserRole> Role { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<HelpRequest> HelpRequest { get; set; }
        public DbSet<Complaint> Complaint { get; set; }
        public DbSet<CanteenMenu> CanteenMenu { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Activity> Activity { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CanteenMenu>()
              .Property(c => c.Price)
              .HasPrecision(18, 2);

            modelBuilder.Entity<Organization>().HasData(
                new Organization
                {
                    Id = 1,
                    Name = "CoSpace Org",
                    PrimaryEmail = "aneeshd70+cospace@gmail.com",
                    SecondaryEmail = "aneeshd70+cospace2@gmail.com",
                    Phone = "6360405023",
                    Domain = "cospace.com",
                    Location = "",
                    OrgLoginKey = "cospace",
                    CreatedBy = 0,
                    CreatedDate = DateTime.Now,
                    UpdatedBy = 0,
                    UpdatedDate = DateTime.Now,
                    IsDeleted = false,
                }
            );


            // Make OrganizationId optional for UserRole
            modelBuilder.Entity<UserRole>()
                .Property(ur => ur.OrganizationId)
                .IsRequired(false);

            modelBuilder.Entity<UserRole>().HasData(
                new UserRole
                {
                    Id = 1,
                    Name = "Super Admin",
                    Description = "Super Admin with all the privileges.",
                    OrganizationId = null, // Explicitly set to null
                    CreatedBy = 0,
                    CreatedDate = DateTime.Now,
                    UpdatedBy = 0,
                    UpdatedDate = DateTime.Now,
                    IsDeleted = false,
                },
                new UserRole
                {
                    Id = 2,
                    Name = "Organization Admin",
                    Description = "Admin with privileges to manage their own organization.",
                    OrganizationId = null, // Explicitly set to null
                    CreatedBy = 0,
                    CreatedDate = DateTime.Now,
                    UpdatedBy = 0,
                    UpdatedDate = DateTime.Now,
                    IsDeleted = false,
                },
                new UserRole
                {
                    Id = 3,
                    Name = "Employee",
                    Description = "Regular employee with limited access.",
                    OrganizationId = null,
                    CreatedBy = 0,
                    CreatedDate = DateTime.Now,
                    UpdatedBy = 0,
                    UpdatedDate = DateTime.Now,
                    IsDeleted = false,
                },
                new UserRole
                {
                    Id = 4,
                    Name = "Guest",
                    Description = "Guest user with very limited access.",
                    OrganizationId = null, // Explicitly set to null
                    CreatedBy = 0,
                    CreatedDate = DateTime.Now,
                    UpdatedBy = 0,
                    UpdatedDate = DateTime.Now,
                    IsDeleted = false,
                }
            );

            modelBuilder.Entity<Admin>().HasData(
               new Admin
               {
                   Id = 1,
                   Username = "super_admin",
                   FirstName = "Aneesh",
                   LastName = "Dembala",
                   Email = "aneeshd70@gmail.com",
                   Phone = "6360405023",
                   Password = "aneesha",
                   RoleId = 1,
                   OrganizationId = 1,
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
