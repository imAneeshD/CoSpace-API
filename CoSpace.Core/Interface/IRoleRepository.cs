using CoSpace.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSpace.Core.Interface
{
    public interface IRoleRepository
    {
        Task<UserRole> AddRole(UserRole role);
        Task<bool> UpdateRole(UserRole role);
        Task<bool> DeleteRole(int id);
        Task<UserRole> GetRole(int id);
        Task<IEnumerable<UserRole>> GetRoles();
    }
}
