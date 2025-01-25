namespace CoSpace.Core.Entities
{
    public class HelpRequest : Base
    {
        public int UserId { get; set; }
        public required string Title { get; set; }
        public string Description { get; set; } = null!;
        public required string Status { get; set; } // e.g., Open, In Progress, Resolved
        public virtual User User { get; set; }
        public DateTime ResolvedAt { get; set; }
    }

}
