using System.Text.Json.Serialization;

namespace CoSpace.Core.Entities
{
    public class Room : Base
    {
        public required string Name { get; set; }
        public required string Type { get; set; } // e.g., Meeting Room, Event Hall, Gaming Room
        public int Capacity { get; set; }
        public string Description { get; set; } = null!;
        public required string Status { get; set; } = "Available"; // e.g., Available, Occupied, Under Maintenance

        [JsonIgnore]
        public ICollection<Booking> Bookings { get; set; }
    }

}
