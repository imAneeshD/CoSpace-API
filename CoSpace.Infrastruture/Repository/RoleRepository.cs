using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using CoSpace.Infrastruture.Data;
using CoSpace.Infrastruture.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSpace.Infrastruture.Repository
{
    public class RoleRepository(ApplicationDbContext dbContext, RepositoryBase<UserRole> repositoryBase, ICurrentUserService currentUserService) : IRoleRepository
    {
        public async Task<UserRole> AddRole(UserRole role)
        {
            repositoryBase.SetAuditFields(role, currentUserService.UserId, "INSERT");
            dbContext.Role.Add(role);
            await dbContext.SaveChangesAsync();
            return role;
        }
        public async Task<bool> UpdateRole(UserRole role)
        {
            var existingRole = await dbContext.Role.FirstOrDefaultAsync(x => x.Id == role.Id);
            if (existingRole != null)
            {
                repositoryBase.SetAuditFields(existingRole, currentUserService.UserId, "UPDATE");

                existingRole.Name = role.Name;

                return await dbContext.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteRole(int id)
        {
            var existingRole = await dbContext.Role.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRole is not null)
            {
                repositoryBase.SetAuditFields(existingRole, currentUserService.UserId, "UPDATE");

                existingRole.IsDeleted = true;

                return await dbContext.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<UserRole> GetRole(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserRole>> GetRoles()
        {
            throw new NotImplementedException();
        }

    }
}
