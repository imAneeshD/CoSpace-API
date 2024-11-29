using CoSpace.Core.Entities;

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
