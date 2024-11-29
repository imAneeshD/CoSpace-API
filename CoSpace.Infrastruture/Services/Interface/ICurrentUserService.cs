namespace CoSpace.Infrastruture.Services.Interface
{
    public interface ICurrentUserService
    {
        int UserId { get; }
        int OrgId { get; }
        int AppAdmin { get; }
        int Role { get; }
    }
}
