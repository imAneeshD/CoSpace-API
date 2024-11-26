using CoSpace.Core.Entities;

namespace CoSpace.Core.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserById(int id);
        Task<User> AddUser(User User);
        Task<bool> UpdateUser(User User);
        Task<bool> DeleteUser(int Id);
        Task<User> Login(string email, string password);
    }
}
