using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSpace.Core.DTO.Admin
{
    public class OrganizationDTO
    {
        public required string Name { get; set; }
        public required string Domain { get; set; }
        public required string PrimaryEmail { get; set; }
        public required string SecondaryEmail { get; set; }
        public required string Phone { get; set; }
        public required string Location { get; set; }
    }
}
