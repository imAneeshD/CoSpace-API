using CoSpace.Core.Entities;

namespace CoSpace.Core.Interface
{
    public interface INotificationRepository
    {
        Task<Notification> AddNotification(Notification Notification);
        Task<bool> UpdateNotification(Notification Notification);
        Task<bool> DeleteNotification(Notification notification);
        Task<Notification> GetNotificationById(int id);
        Task<IEnumerable<Notification>> GetNotifications();
    }
}
