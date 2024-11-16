using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoSpace.Core.Entities
{
    public class UserType : Base
    {
        public required string Name { get; set; }

        public virtual Organization Organization { get; set; }  
    }
}
