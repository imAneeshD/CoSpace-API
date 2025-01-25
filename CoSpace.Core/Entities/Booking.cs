using System.Text.Json.Serialization;

namespace CoSpace.Core.Entities
{
    public class Booking : Base
    {
        public int RoomId { get; set; }
        public int UserId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public required string Status { get; set; } // e.g., Pending, Confirmed, Cancelled

        [JsonIgnore]
        public virtual User? User { get; set; }

        [JsonIgnore]
        public virtual Room? Room { get; set; } 
    }
}
