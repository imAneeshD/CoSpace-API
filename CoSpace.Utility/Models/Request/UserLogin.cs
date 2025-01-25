namespace CoSpace.Utility.Models.Request
{
    public class UserLogin
    {
        public string OrgID { get; set; } = null!;
        public required string OrgName { get; set; } = "";
        public required string Email { get; set; } = "";
        public required string Password { get; set; } = "";
    }
}
