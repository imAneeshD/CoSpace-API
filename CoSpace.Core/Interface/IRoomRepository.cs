using CoSpace.Core.Entities;

namespace CoSpace.Core.Interface
{
    public interface IRoomRepository
    {
        Task<Room> AddRoom(Room Room);
        Task<bool> UpdateRoom(Room Room);
        Task<bool> DeleteRoom(int id);
        Task<Room> GetRoomById(int id);
        Task<IEnumerable<Room>> GetRooms();
    }
}
