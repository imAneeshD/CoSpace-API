using CoSpace.Core.Entities;

namespace CoSpace.Core.Interface
{
    public interface ICanteenRepository
    {
        Task<CanteenMenu> AddCanteenMenu(CanteenMenu CanteenMenu);
        Task<bool> UpdateCanteenMenu(CanteenMenu CanteenMenu);
        Task<bool> DeleteCanteenMenu(int id);
        Task<CanteenMenu> GetCanteenMenuById(int id);
        Task<IEnumerable<CanteenMenu>> GetCanteenMenus();
    }
}
