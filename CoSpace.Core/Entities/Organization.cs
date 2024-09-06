using System.ComponentModel.DataAnnotations;

namespace CoSpace.Core.Entities
{
    public class Organization : Base
    {
        public required string Name { get; set; }
        public required string Domain { get; set; }
        public required string PrimaryEmail { get; set; }
        public required string SecondaryEmail { get; set; }
        public required string Phone { get; set; }
        public required string Location { get; set; }

    }
}
