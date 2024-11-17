using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using CoSpace.Infrastruture.Data;
using CoSpace.Infrastruture.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace CoSpace.Infrastruture.Repository
{
    public class OrganizationRepository(ApplicationDbContext dbContext, RepositoryBase<Organization> repositoryBase, ICurrentUserService currentUserService) : IOrganizationRepository
    {
        public async Task<Organization> AddOrganization(Organization organization)
        {
            repositoryBase.SetAuditFields(organization, currentUserService.UserId, "INSERT");

            dbContext.Organization.Add(organization);
            await dbContext.SaveChangesAsync();
            return organization;
        }

        public async Task<bool> UpdateOrganization(Organization organization)
        {
            var existingRecord = await dbContext.Organization.FirstOrDefaultAsync(x => x.Id == organization.Id);
            if (existingRecord != null)
            {
                repositoryBase.SetAuditFields(existingRecord, currentUserService.UserId, "UPDATE");

                existingRecord.Location = organization.Location;
                existingRecord.Domain = organization.Domain;
                existingRecord.Name = organization.Name;
                existingRecord.PrimaryEmail = organization.PrimaryEmail;
                existingRecord.SecondaryEmail = organization.SecondaryEmail;

                return await dbContext.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<IEnumerable<Organization>> GetOrganizations()
        {
            return await dbContext.Organization.ToListAsync();
        }

        public async Task<bool> DeleteOrganization(int id)
        {
            var organization = await dbContext.Organization.FirstOrDefaultAsync(x => x.Id == id);

            if (organization is not null)
            {
                organization.IsDeleted = true;

                return await dbContext.SaveChangesAsync() > 0;
            }

            return false;
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
    }
}
