using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoSpace.Core.Entities
{
    [Index("Name", IsUnique = true)]
    public class UserRole : Base
    {
        public required string Name { get; set; }
    }
}
