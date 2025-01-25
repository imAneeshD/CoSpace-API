using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using CoSpace.Infrastruture.Data;
using CoSpace.Infrastruture.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace CoSpace.Infrastruture.Repository
{
    public class UserRoleRepository(ApplicationDbContext dbContext, RepositoryBase<UserRole> repositoryBase, ICurrentUserService currentUserService) : IUserRoleRepository
    {
        public async Task<UserRole> AddUserRole(UserRole role)
        {
            repositoryBase.SetAuditFields(role, currentUserService.UserId, "INSERT");
            dbContext.Role.Add(role);
            await dbContext.SaveChangesAsync();
            return role;
        }
        public async Task<bool> UpdateUserRole(UserRole role)
        {
            var existingRole = await dbContext.Role.FirstOrDefaultAsync(x => x.Id == role.Id);
            if (existingRole != null)
            {
                repositoryBase.SetAuditFields(existingRole, currentUserService.UserId, "UPDATE");

                existingRole.Name = role.Name;
                existingRole.Description = role.Description;

                return await dbContext.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteUserRole(UserRole role)
        {

            repositoryBase.SetAuditFields(role, currentUserService.UserId, "UPDATE");

            role.IsDeleted = true;

            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<UserRole> GetUserRole(int id)
        {
            return await dbContext.Role.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<UserRole>> GetUserRoles()
        {
            return await dbContext.Role.Where(x => x.IsDeleted == false).ToListAsync();
        }

    }
}
