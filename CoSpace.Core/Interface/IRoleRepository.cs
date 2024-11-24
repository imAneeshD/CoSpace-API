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
        Task<Role> AddRole(Role role);
        Task<bool> UpdateRole(Role role);
        Task<bool> DeleteRole(int id);
        Task<Role> GetRole(int id);
        Task<IEnumerable<Role>> GetRoles();
    }
}
