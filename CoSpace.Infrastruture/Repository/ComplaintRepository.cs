using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using CoSpace.Infrastruture.Data;
using CoSpace.Infrastruture.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace CoSpace.Infrastruture.Repository
{
    public class ComplaintRepository(ApplicationDbContext dbContext, ICurrentUserService currentUserService, RepositoryBase<Complaint> repositoryBase) : IComplaintRepository
    {
        public async Task<Complaint> AddComplaint(Complaint Complaint)
        {
            repositoryBase.SetAuditFields(Complaint, currentUserService.UserId, "INSERT");

            dbContext.Complaint.Add(Complaint);
            await dbContext.SaveChangesAsync();
            return Complaint;
        }

        public async Task<bool> UpdateComplaint(Complaint Complaint)
        {
            var existingComplaint = await dbContext.Complaint.FirstOrDefaultAsync(x => x.Id == Complaint.Id);
            if (existingComplaint != null)
            {
                repositoryBase.SetAuditFields(existingComplaint, currentUserService.UserId, "UPDATE");

                existingComplaint.Title = Complaint.Title;
                existingComplaint.Description = Complaint.Description;
                existingComplaint.Status = Complaint.Status;
                existingComplaint.OrganizationId = Complaint.OrganizationId;

                return await dbContext.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteComplaint(int id)
        {
            var existingComplaint = await dbContext.Complaint.FirstOrDefaultAsync(x => x.Id == id);

            if (existingComplaint is not null)
            {
                repositoryBase.SetAuditFields(existingComplaint, currentUserService.UserId, "DELETE");

                return await dbContext.SaveChangesAsync() > 0;
            }

            return false;
        }
        public async Task<Complaint> GetComplaintById(int id)
        {
            var Complaint = await dbContext.Complaint.FirstOrDefaultAsync(x => x.Id == id);

            if (Complaint is not null)
            {
                return Complaint;
            }

            return null;
        }

        public async Task<IEnumerable<Complaint>> GetComplaints()
        {
            return await dbContext.Complaint.ToListAsync();
        }

    }
}
