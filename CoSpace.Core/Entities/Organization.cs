using Microsoft.EntityFrameworkCore;

namespace CoSpace.Core.Entities
{
    [Index("Name", IsUnique = true)]
    [Index("Domain", IsUnique = true)]
    public class Organization
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Domain { get; set; }
        public required string PrimaryEmail { get; set; }
        public string SecondaryEmail { get; set; } = null!;
        public required string Phone { get; set; }
        public required string Location { get; set; } = null!;


        // Audit
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; } = 1;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int UpdatedBy { get; set; } = 1;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

    }
}
