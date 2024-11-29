using CoSpace.Core.Entities;

namespace CoSpace.Core.DTO
{
    public class CanteenMenuDTO : Base
    {
        public required string ItemName { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime AvailableFrom { get; set; }
        public DateTime AvailableTo { get; set; }
    }
}
