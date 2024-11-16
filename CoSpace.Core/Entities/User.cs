using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSpace.Core.Entities
{
    [Index("Username", IsUnique =true)]
    public class User : Base
    {
        public required string Username {  get; set; }
        public required string FirstName {  get; set; }
        public string LastName { get; set; } = null!;
        public required string Email {  get; set; }
        public required string Password {  get; set; }

        public virtual Organization Organization { get; set; }
        public virtual Role Role { get; set; }
        public virtual UserType UserType { get; set; }
    }
}
