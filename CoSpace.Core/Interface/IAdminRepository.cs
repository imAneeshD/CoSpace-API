using CoSpace.Core.Entities;

namespace CoSpace.Core.Interface
{
    public interface IAdminRepository
    {
        Task<IEnumerable<Admin>> GetAdmins();
        Task<Admin> GetAdminById(int id);
        Task<Admin> AddAdmin(Admin Admin);
        Task<bool> UpdateAdmin(Admin Admin);
        Task<bool> DeleteAdmin(Admin admin);
        Task<Admin> Login(string email, string password);
        Task<DashboardStats> GetAdminStats();
    }
}
