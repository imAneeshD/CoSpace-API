namespace CoSpace.Core.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public required int UserId { get; set; }
        public required int OrganizationId { get; set; }
        public required string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Revoked { get; set; }

        public virtual Organization? Organization { get; set; }
    }
}
