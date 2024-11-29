using CoSpace.Core.Entities;

namespace CoSpace.Core.Interface
{
    public interface IOrganizationRepository
    {
        Task<Organization> AddOrganization(Organization organization);
        Task<bool> UpdateOrganization(Organization organization);
        Task<bool> DeleteOrganization(int id);
        Task<Organization> GetOrganizationById(int id);
        Task<IEnumerable<Organization>> GetOrganizations();
    }
}
