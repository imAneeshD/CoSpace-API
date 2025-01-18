using CoSpace.Core.Entities;

namespace CoSpace.Core.DTO
{
    public class UserCreateDTO
    {
        public required string Username { get; set; }
        public required string FirstName { get; set; }
        public string LastName { get; set; } = null!;
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string Password { get; set; }
        public required int RoleId { get; set; }
        public required int OrganizationId { get; set; }
    }

    public class UserRetrieveDTO
    {
        public required string Username { get; set; }
        public required string FirstName { get; set; }
        public string LastName { get; set; } = null!;
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required int RoleId { get; set; }
        public required int OrganizationId { get; set; }

        public virtual UserRole Role { get; set; } = null!;
        public virtual Organization Organization { get; set; } = null!;

    }
}
