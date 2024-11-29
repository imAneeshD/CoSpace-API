using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoSpace.Core.Entities
{
    public class CanteenMenu : Base
    {
        public required string ItemName { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime AvailableFrom { get; set; }
        public DateTime AvailableTo { get; set; }
    }
}
