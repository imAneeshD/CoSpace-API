using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoSpace.Core.Entities
{
    public class AppUserType : Base
    {
        public required string Name { get; set; }
    }
}
