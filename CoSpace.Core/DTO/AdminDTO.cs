namespace CoSpace.Core.DTO
{
    public class AdminDTO
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string FirstName { get; set; }
        public string LastName { get; set; } = null!;
    }
}
