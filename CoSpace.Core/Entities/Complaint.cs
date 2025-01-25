namespace CoSpace.Core.Entities
{
    public class Complaint : Base
    {
        public int UserId { get; set; }
        public required string Title { get; set; }
        public string Description { get; set; } = null!;
        public required string Status { get; set; } // e.g., Open, In Progress, Resolved
        public virtual User User { get; set; }
    }
}
