using System.ComponentModel.DataAnnotations;

namespace CoSpace.Core.Entities
{
    public class Organization : Base
    {
        [Key]
        public required string Name { get; set; }
        [Key]
        public required string Domain { get; set; }
        [Key]
        public required string PrimaryEmail { get; set; }
        public required string SecondaryEmail { get; set; }
        public required string Phone { get; set; }
        public required string Location { get; set; }

    }
}
