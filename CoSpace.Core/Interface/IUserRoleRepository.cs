using CoSpace.Core.Entities;

namespace CoSpace.Core.Interface
{
    public interface IUserRoleRepository
    {
        Task<UserRole> AddUserRole(UserRole role);
        Task<bool> UpdateUserRole(UserRole role);
        Task<bool> DeleteUserRole(UserRole id);
        Task<UserRole> GetUserRole(int id);
        Task<IEnumerable<UserRole>> GetUserRoles();
    }
}
