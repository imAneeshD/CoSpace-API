using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSpace.Core.Entities
{
    public class Base
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; } = 1; 
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int UpdatedBy { get; set; } = 1;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}
