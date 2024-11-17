using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using CoSpace.Infrastruture.Data;
using CoSpace.Infrastruture.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace CoSpace.Infrastruture.Repository
{
    public class AdminRepository(ApplicationDbContext dbContext, RepositoryBase<Admin> repositoryBase, ICurrentUserService currentUserService) : IAdminRepository
    {
        public async Task<IEnumerable<Admin>> GetAdmins()
        {
            return await dbContext.Admin.ToListAsync();
        }

        public async Task<Admin> GetAdminById(int id)
        {
            return await dbContext.Admin.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Admin> AddAdmin(Admin admin)
        {
            repositoryBase.SetAuditFields(admin, currentUserService.UserId, "INSERT");

            dbContext.Admin.Add(admin);

            await dbContext.SaveChangesAsync();

            return admin;
        }

        public async Task<bool> UpdateAdmin(Admin admin)
        {
            var existingRecord = await dbContext.Admin.FirstOrDefaultAsync(x => x.Id == admin.Id);

            if (existingRecord is not null)
            {
                repositoryBase.SetAuditFields(existingRecord, currentUserService.UserId, "UPDATE");

                existingRecord.FirstName = admin.FirstName;
                existingRecord.LastName = admin.LastName;
                existingRecord.Email = admin.Email;
                existingRecord.Password = admin.Password;

                return await dbContext.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteAdmin(int id)
        {
            var existingRecord = await dbContext.Admin.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRecord is not null)
            {
                existingRecord.IsDeleted = true;

                return await dbContext.SaveChangesAsync() > 0;
            }

            return false;
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
