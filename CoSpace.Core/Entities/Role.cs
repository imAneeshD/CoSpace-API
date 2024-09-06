using System.ComponentModel.DataAnnotations.Schema;

namespace CoSpace.Core.Entities
{
    public class Role : Base
    {
        public required string RoleName { get; set; }
        public required int OrganizationID { get; set; }
        [ForeignKey(nameof(OrganizationID))]
        public virtual Organization Organization { get; set; }  
    }
}
