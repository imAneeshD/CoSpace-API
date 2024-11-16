using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using CoSpace.Infrastruture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoSpace.Infrastruture.Repository
{
    public class OrganizationRepository(ApplicationDbContext dbContext) : IOrganizationRepository
    {
        public async Task<Organization> AddOrganization(Organization organization)
        {
            dbContext.Organization.Add(organization);
            await dbContext.SaveChangesAsync();
            return organization;
        }

        public async Task<bool> UpdateOrganization(Organization organization)
        {
            dbContext.Organization.Update(organization);
            if(await dbContext.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }
    }
}
