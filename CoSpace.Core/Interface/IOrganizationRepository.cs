using CoSpace.Core.Entities;

namespace CoSpace.Core.Interface
{
    public interface IOrganizationRepository
    {
        Task<Organization> AddOrganization(Organization organization);
        Task<bool> UpdateOrganization(Organization organization);
        Task<bool> DeleteOrganization(Organization organization);
        Task<Organization> GetOrganizationById(int id);
        Task<IEnumerable<Organization>> GetOrganizations();
        Task<Organization> GetOrganizationByName(string name);
    }
}
