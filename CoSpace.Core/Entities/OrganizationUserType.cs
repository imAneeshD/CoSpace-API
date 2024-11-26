namespace CoSpace.Core.Entities
{
    public class OrganizationUserType : Base
    {
        public required string Name { get; set; }

        public virtual Organization Organization { get; set; }
    }
}
