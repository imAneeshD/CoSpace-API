using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using CoSpace.Infrastruture.Data;
using CoSpace.Infrastruture.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace CoSpace.Infrastruture.Repository
{
    public class OrganizationRepository(ApplicationDbContext dbContext, ICurrentUserService currentUserService) : IOrganizationRepository
    {
        public async Task<Organization> AddOrganization(Organization organization)
        {
            //repositoryBase.SetAuditFields(organization, currentUserService.UserId, "INSERT");

            // Need Audit Fields in Organization Entity
            organization.UpdatedBy = currentUserService.UserId;
            organization.UpdatedDate = DateTime.Now;
            organization.CreatedBy = currentUserService.UserId;
            organization.CreatedDate = DateTime.Now;

            dbContext.Organization.Add(organization);

            await dbContext.SaveChangesAsync();
            return organization;
        }

        public async Task<bool> UpdateOrganization(Organization organization)
        {
            var existingOrganization = await dbContext.Organization.FirstOrDefaultAsync(x => x.Id == organization.Id);
            if (existingOrganization != null)
            {
                //repositoryBase.SetAuditFields(existingOrganization, currentUserService.UserId, "UPDATE");

                // Need Audit Fields in Organization Entity

                existingOrganization.OrgLoginKey = organization.OrgLoginKey;
                existingOrganization.Location = organization.Location;
                existingOrganization.Domain = organization.Domain;
                existingOrganization.Name = organization.Name;
                existingOrganization.PrimaryEmail = organization.PrimaryEmail;
                existingOrganization.SecondaryEmail = organization.SecondaryEmail;
                existingOrganization.UpdatedBy = currentUserService.UserId;
                existingOrganization.UpdatedDate = DateTime.Now;

                return await dbContext.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteOrganization(Organization existingOrganization)
        {
            //repositoryBase.SetAuditFields(existingOrganization, currentUserService.UserId, "UPDATE");
            existingOrganization.UpdatedBy = currentUserService.UserId;
            existingOrganization.UpdatedDate = DateTime.Now;

            existingOrganization.IsDeleted = true;

            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<Organization> GetOrganizationById(int id)
        {
            var organization = await dbContext.Organization.FirstOrDefaultAsync(x => x.Id == id);

            if (organization is not null)
            {
                return organization;
            }

            return null;
        }

        public async Task<IEnumerable<Organization>> GetOrganizations()
        {
            return await dbContext.Organization.ToListAsync();
        }

        public Task<Organization> GetOrganizationByName(string name)
        {
            return dbContext.Organization.FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
