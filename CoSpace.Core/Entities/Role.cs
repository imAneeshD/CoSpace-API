using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
