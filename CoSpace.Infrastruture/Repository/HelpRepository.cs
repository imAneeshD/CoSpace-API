using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using CoSpace.Infrastruture.Data;
using CoSpace.Infrastruture.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace CoSpace.Infrastruture.Repository
{
    public class HelpRepository(ApplicationDbContext dbContext, ICurrentUserService currentUserService, RepositoryBase<HelpRequest> repositoryBase) : IHelpRepository
    {
        public async Task<HelpRequest> AddHelpRequest(HelpRequest HelpRequest)
        {
            repositoryBase.SetAuditFields(HelpRequest, currentUserService.UserId, "INSERT");

            dbContext.HelpRequest.Add(HelpRequest);
            await dbContext.SaveChangesAsync();
            return HelpRequest;
        }

        public async Task<bool> UpdateHelpRequest(HelpRequest HelpRequest)
        {
            var existingHelpRequest = await dbContext.HelpRequest.FirstOrDefaultAsync(x => x.Id == HelpRequest.Id);
            if (existingHelpRequest != null)
            {
                repositoryBase.SetAuditFields(existingHelpRequest, currentUserService.UserId, "UPDATE");

                existingHelpRequest.UserId = HelpRequest.UserId;
                existingHelpRequest.UserId = HelpRequest.UserId;
                existingHelpRequest.Title = HelpRequest.Title;
                existingHelpRequest.Description = HelpRequest.Description;
                existingHelpRequest.Status = HelpRequest.Status;
                existingHelpRequest.OrganizationId = HelpRequest.OrganizationId;

                return await dbContext.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteHelpRequest(HelpRequest helpRequest)
        {
            repositoryBase.SetAuditFields(helpRequest, currentUserService.UserId, "DELETE");
            helpRequest.IsDeleted = true;
            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<HelpRequest> GetHelpRequestById(int id)
        {
            var HelpRequest = await dbContext.HelpRequest.FirstOrDefaultAsync(x => x.Id == id);

            if (HelpRequest is not null)
            {
                return HelpRequest;
            }

            return null;
        }

        public async Task<IEnumerable<HelpRequest>> GetHelpRequests()
        {
            return await dbContext.HelpRequest.ToListAsync();
        }

    }
}
