using Microsoft.EntityFrameworkCore;

namespace CoSpace.Core.Entities
{
    [Index("Username", IsUnique = true)]
    public class User : Base
    {
        public required string Username { get; set; }
        public required string FirstName { get; set; }
        public string LastName { get; set; } = null!;
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string Password { get; set; }
        public required int RoleId { get; set; }

        public virtual UserRole Role { get; set; }
    }
}
