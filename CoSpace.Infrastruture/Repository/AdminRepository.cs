using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using CoSpace.Infrastruture.Data;
using CoSpace.Infrastruture.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace CoSpace.Infrastruture.Repository
{
    public class AdminRepository(ApplicationDbContext dbContext, RepositoryBase<Admin> repositoryBase, ICurrentUserService currentAdminService) : IAdminRepository
    {
        public async Task<Admin> AddAdmin(Admin Admin)
        {
            repositoryBase.SetAuditFields(Admin, currentAdminService.UserId, "INSERT");

            dbContext.Admin.Add(Admin);

            await dbContext.SaveChangesAsync();

            return Admin;
        }

        public async Task<bool> UpdateAdmin(Admin Admin)
        {
            var existingAdmin = await dbContext.Admin.FirstOrDefaultAsync(x => x.Id == Admin.Id);

            if (existingAdmin is not null)
            {
                repositoryBase.SetAuditFields(existingAdmin, currentAdminService.UserId, "UPDATE");

                existingAdmin.FirstName = Admin.FirstName;
                existingAdmin.LastName = Admin.LastName;
                existingAdmin.Email = Admin.Email;
                existingAdmin.Password = Admin.Password;

                return await dbContext.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteAdmin(int id)
        {
            var existingAdmin = await dbContext.Admin.FirstOrDefaultAsync(x => x.Id == id);

            if (existingAdmin is not null)
            {
                repositoryBase.SetAuditFields(existingAdmin, currentAdminService.UserId, "DELETE");

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
