using CoSpace.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSpace.Core.Interface
{
    public interface IOrganizationRepository
    {
        Task<Organization> AddOrganization(Organization organization);
        Task<IEnumerable<Organization>> GetOrganizations();
        Task<bool> UpdateOrganization(Organization organization);
        Task<bool> DeleteOrganization(int id);
        Task<Organization> GetOrganizationById(int id);
    }
}
