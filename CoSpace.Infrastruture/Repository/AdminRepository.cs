using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using CoSpace.Infrastruture.Data;
using CoSpace.Infrastruture.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace CoSpace.Infrastruture.Repository
{
    public class AdminRepository(ApplicationDbContext dbContext, RepositoryBase<Admin> repositoryBase, ICurrentUserService currentUserService) : IAdminRepository
    {
        public async Task<Admin> AddAdmin(Admin admin)
        {
            repositoryBase.SetAuditFields(admin, currentUserService.UserId, "INSERT");

            dbContext.Admin.Add(admin);

            await dbContext.SaveChangesAsync();

            return admin;
        }

        public async Task<bool> UpdateAdmin(Admin admin)
        {
            var existingAdmin = await dbContext.Admin.FirstOrDefaultAsync(x => x.Id == admin.Id);

            if (existingAdmin is not null)
            {
                repositoryBase.SetAuditFields(existingAdmin, currentUserService.UserId, "UPDATE");

                existingAdmin.FirstName = admin.FirstName;
                existingAdmin.LastName = admin.LastName;
                existingAdmin.Email = admin.Email;
                existingAdmin.Password = admin.Password;

                return await dbContext.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteAdmin(int id)
        {
            var existingAdmin = await dbContext.Admin.FirstOrDefaultAsync(x => x.Id == id);

            if (existingAdmin is not null)
            {
                repositoryBase.SetAuditFields(existingAdmin, currentUserService.UserId, "DELETE");

                existingAdmin.IsDeleted = true;

                return await dbContext.SaveChangesAsync() > 0;
            }

            return false;
        }
        public async Task<Admin> GetAdminById(int id)
        {
            return await dbContext.Admin.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Admin>> GetAdmins()
        {
            return await dbContext.Admin.ToListAsync();
        }

        public async Task<Admin> Login(string email, string password)
        {
            var result = await dbContext.Admin.FirstOrDefaultAsync(x => x.Email == email && x.Password == password && x.IsDeleted == false);
            if (result is not null)
            {
                return result;
            }
            return null;
        }
    }
}
