using System.ComponentModel.DataAnnotations;

namespace CoSpace.Core.Entities
{
    public class Admin : Base
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public required string FirstName {  get; set; }
        public string LastName { get; set; } = null!;
    }
}
