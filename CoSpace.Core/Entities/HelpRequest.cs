using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoSpace.Core.Entities
{
    public class HelpRequest : Base
    {
        public int UserId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Status { get; set; } // e.g., Open, In Progress, Resolved
        public virtual User User { get; set; }
    }

}
