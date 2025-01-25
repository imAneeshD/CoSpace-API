using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using CoSpace.Infrastruture.Data;
using CoSpace.Infrastruture.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace CoSpace.Infrastruture.Repository
{
    public class NotificationRepository(ApplicationDbContext dbContext, ICurrentUserService currentUserService, RepositoryBase<Notification> repositoryBase) : INotificationRepository
    {
        public async Task<Notification> AddNotification(Notification Notification)
        {
            repositoryBase.SetAuditFields(Notification, currentUserService.UserId, "INSERT");

            dbContext.Notification.Add(Notification);
            await dbContext.SaveChangesAsync();
            return Notification;
        }

        public async Task<bool> UpdateNotification(Notification Notification)
        {
            var existingNotification = await dbContext.Notification.FirstOrDefaultAsync(x => x.Id == Notification.Id);
            if (existingNotification != null)
            {
                repositoryBase.SetAuditFields(existingNotification, currentUserService.UserId, "UPDATE");

                existingNotification.Title = Notification.Title;
                existingNotification.Message = Notification.Message;
                existingNotification.Type = Notification.Type;
                existingNotification.Priority = Notification.Priority;
                //existingNotification.ReadAt = Notification.ReadAt;
                //existingNotification.IsRead = Notification.IsRead;    
                existingNotification.OrganizationId = Notification.OrganizationId;

                return await dbContext.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteNotification(int id)
        {
            var existingNotification = await dbContext.Notification.FirstOrDefaultAsync(x => x.Id == id);

            if (existingNotification is not null)
            {
                repositoryBase.SetAuditFields(existingNotification, currentUserService.UserId, "DELETE");

                return await dbContext.SaveChangesAsync() > 0;
            }

            return false;
        }
        public async Task<Notification> GetNotificationById(int id)
        {
            var Notification = await dbContext.Notification.FirstOrDefaultAsync(x => x.Id == id);

            if (Notification is not null)
            {
                return Notification;
            }

            return null;
        }

        public async Task<IEnumerable<Notification>> GetNotifications()
        {
            return await dbContext.Notification.ToListAsync();
        }

    }
}
