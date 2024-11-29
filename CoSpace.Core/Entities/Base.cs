namespace CoSpace.Core.Entities
{
    public class Base
    {
        public int Id { get; set; }
        public required int? OrganizationId { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; } = 1;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int UpdatedBy { get; set; } = 1;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        public virtual Organization Organization { get; set; }
    }
}
