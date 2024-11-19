using CoSpace.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSpace.Core.Interface
{
    public interface IAdminRepository
    {
        Task<IEnumerable<Admin>> GetAdmins();
        Task<Admin> GetAdminById(int id);
        Task<Admin> AddAdmin(Admin admin);
        Task<bool> UpdateAdmin(Admin admin);
        Task<bool> DeleteAdmin(int Id);
        Task<Admin> Login(string email, string password);
    }
}
