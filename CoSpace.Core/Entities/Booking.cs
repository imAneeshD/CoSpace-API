using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoSpace.Core.Entities
{
    public class Booking : Base
    {
        public int RoomId { get; set; }
        public int UserId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public required string Status { get; set; } // e.g., Pending, Confirmed, Cancelled

        public virtual User User { get; set; }
        public virtual Room Room { get; set; }
    }

}
