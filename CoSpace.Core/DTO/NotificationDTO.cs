using CoSpace.Core.Entities;

namespace CoSpace.Core.DTO
{
    public class NotificationDTO : Base
    {
        public required string Title { get; set; }
        public required string Message { get; set; }
        public required string Type { get; set; }
        public required string Priority { get; set; }
        public DateTime ReadAt { get; set; }
        public required string IsRead { get; set; }
        public string URL { get; set; } = string.Empty;
    }
}
