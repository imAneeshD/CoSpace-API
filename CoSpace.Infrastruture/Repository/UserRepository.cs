using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using CoSpace.Infrastruture.Data;
using CoSpace.Infrastruture.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace CoSpace.Infrastruture.Repository
{
    public class UserRepository(ApplicationDbContext dbContext, RepositoryBase<User> repositoryBase, ICurrentUserService currentUserService) : IUserRepository
    {
        public async Task<User> AddUser(User User)
        {
            repositoryBase.SetAuditFields(User, currentUserService.UserId, "INSERT");

            dbContext.User.Add(User);

            await dbContext.SaveChangesAsync();

            return User;
        }

        public async Task<bool> UpdateUser(User User)
        {
            var existingUser = await dbContext.User.FirstOrDefaultAsync(x => x.Id == User.Id);

            if (existingUser is not null)
            {
                repositoryBase.SetAuditFields(existingUser, currentUserService.UserId, "UPDATE");

                existingUser.FirstName = User.FirstName;
                existingUser.LastName = User.LastName;
                existingUser.Email = User.Email;
                existingUser.Password = User.Password;

                return await dbContext.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteUser(User user)
        {

            repositoryBase.SetAuditFields(user, currentUserService.UserId, "DELETE");

            user.IsDeleted = true;

            return await dbContext.SaveChangesAsync() > 0;

        }

        public async Task<User> GetUserById(int id)
        {
            return await dbContext.User.Include(x => x.Role).Include(x => x.Organization).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await dbContext.User.Include(x => x.Role).Include(x => x.Organization).ToListAsync();
        }

        public async Task<User> Login(string email, string password, string OrgId)
        {
            var result = await dbContext.User.FirstOrDefaultAsync(x => x.OrganizationId == int.Parse(OrgId) && x.Email == email && x.Password == password && x.IsDeleted == false);
            if (result is not null)
            {
                return result;
            }
            return null;
        }
    }
}
