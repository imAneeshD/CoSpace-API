namespace CoSpace.Core.DTO
{
    public class BookingDTO
    {
        public int RoomId { get; set; }
        public int UserId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public required string Status { get; set; } // e.g., Pending, Confirmed, Cancelled
    }
}
