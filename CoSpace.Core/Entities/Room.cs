using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoSpace.Core.Entities
{
    public class Room : Base
    {
        public required string Name { get; set; }
        public required string Type { get; set; } // e.g., Meeting Room, Event Hall, Gaming Room
        public int Capacity { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }

}
