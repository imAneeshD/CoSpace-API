using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using CoSpace.Infrastruture.Data;
using CoSpace.Infrastruture.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace CoSpace.Infrastruture.Repository
{
    public class RoomRepository(ApplicationDbContext dbContext, ICurrentUserService currentUserService, RepositoryBase<Room> repositoryBase) : IRoomRepository
    {
        public async Task<Room> AddRoom(Room Room)
        {
            repositoryBase.SetAuditFields(Room, currentUserService.UserId, "INSERT");

            dbContext.Room.Add(Room);
            await dbContext.SaveChangesAsync();
            return Room;
        }

        public async Task<bool> UpdateRoom(Room Room)
        {
            var existingRoom = await dbContext.Room.FirstOrDefaultAsync(x => x.Id == Room.Id);
            if (existingRoom != null)
            {
                repositoryBase.SetAuditFields(existingRoom, currentUserService.UserId, "UPDATE");

                existingRoom.Name = Room.Name;
                existingRoom.Type = Room.Type;
                existingRoom.Capacity = Room.Capacity;
                existingRoom.Description = Room.Description;
                existingRoom.Status = Room.Status;
                existingRoom.OrganizationId= Room.OrganizationId;

                return await dbContext.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteRoom(int id)
        {
            var existingRoom = await dbContext.Room.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRoom is not null)
            {
                repositoryBase.SetAuditFields(existingRoom, currentUserService.UserId, "DELETE");

                return await dbContext.SaveChangesAsync() > 0;
            }

            return false;
        }
        public async Task<Room> GetRoomById(int id)
        {
            var Room = await dbContext.Room.FirstOrDefaultAsync(x => x.Id == id);

            if (Room is not null)
            {
                return Room;
            }

            return null;
        }

        public async Task<IEnumerable<Room>> GetRooms()
        {
            return await dbContext.Room.ToListAsync();
        }

    }
}
