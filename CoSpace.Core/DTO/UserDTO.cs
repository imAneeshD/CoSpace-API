namespace CoSpace.Core.DTO
{
    public class UserDTO
    {
        public required string Username { get; set; }
        public required string FirstName { get; set; }
        public string LastName { get; set; } = null!;
        public required string Email { get; set; }
    }
}
