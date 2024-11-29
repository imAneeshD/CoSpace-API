using CoSpace.Core.Entities;

namespace CoSpace.Core.DTO
{
    public class RoomDTO : Base
    {
        public required string Name { get; set; }
        public required string Type { get; set; } // e.g., Meeting Room, Event Hall, Gaming Room
        public int Capacity { get; set; }
    }
}
