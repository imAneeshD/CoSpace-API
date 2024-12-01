using Microsoft.EntityFrameworkCore;

namespace CoSpace.Core.Entities
{
    [Index("Username", IsUnique = true)]
    public class Admin : Base
    {
        public required string Username { get; set; }
        public required string FirstName { get; set; }
        public string LastName { get; set; } = null!;
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string Password { get; set; }
        public required int RoleId { get; set; }

        //ForeignKey Table
        public virtual UserRole? Role { get; set; }
    }
}
