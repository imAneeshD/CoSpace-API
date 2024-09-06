using CoSpace.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSpace.Infrastruture.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Organization> Organization { get; set; }
        public DbSet<Role> Role { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Organization>().HasKey(x =>
            new {
                x.PrimaryEmail,
                x.Domain,
                x.Name
            });

            modelBuilder.Entity<Admin>().HasKey(x => 
            new {
                x.Username,
                x.Email
            });

            // Seed default Admin
            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    Id = 1,
                    Username = "admin",
                    Password = "admin123",  
                    Email = "admin@gmail.com",
                    FirstName = "Default",
                    LastName = "Admin",
                }
            );
        }
    }
}
