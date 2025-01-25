using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using CoSpace.Infrastruture.Data;
using CoSpace.Infrastruture.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace CoSpace.Infrastruture.Repository
{
    public class CanteenMenuRepository(ApplicationDbContext dbContext, ICurrentUserService currentUserService, RepositoryBase<CanteenMenu> repositoryBase) : ICanteenRepository
    {
        public async Task<CanteenMenu> AddCanteenMenu(CanteenMenu CanteenMenu)
        {
            repositoryBase.SetAuditFields(CanteenMenu, currentUserService.UserId, "INSERT");

            dbContext.CanteenMenu.Add(CanteenMenu);
            await dbContext.SaveChangesAsync();
            return CanteenMenu;
        }

        public async Task<bool> UpdateCanteenMenu(CanteenMenu CanteenMenu)
        {
            var existingCanteenMenu = await dbContext.CanteenMenu.FirstOrDefaultAsync(x => x.Id == CanteenMenu.Id);
            if (existingCanteenMenu != null)
            {
                repositoryBase.SetAuditFields(existingCanteenMenu, currentUserService.UserId, "UPDATE");

                existingCanteenMenu.ItemName = CanteenMenu.ItemName;
                existingCanteenMenu.Description = CanteenMenu.Description;
                existingCanteenMenu.Price = CanteenMenu.Price;
                existingCanteenMenu.AvailableFrom = CanteenMenu.AvailableFrom;
                existingCanteenMenu.AvailableTo = CanteenMenu.AvailableTo;
                existingCanteenMenu.OrganizationId = CanteenMenu.OrganizationId;



                return await dbContext.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteCanteenMenu(CanteenMenu canteenMenu)
        {

            repositoryBase.SetAuditFields(canteenMenu, currentUserService.UserId, "DELETE");
            canteenMenu.IsDeleted = true;
            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<CanteenMenu> GetCanteenMenuById(int id)
        {
            var CanteenMenu = await dbContext.CanteenMenu.FirstOrDefaultAsync(x => x.Id == id);

            if (CanteenMenu is not null)
            {
                return CanteenMenu;
            }

            return null;
        }

        public async Task<IEnumerable<CanteenMenu>> GetCanteenMenus()
        {
            return await dbContext.CanteenMenu.ToListAsync();
        }

    }
}
