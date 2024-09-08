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
    }
}
