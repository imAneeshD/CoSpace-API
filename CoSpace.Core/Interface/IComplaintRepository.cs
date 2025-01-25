using CoSpace.Core.Entities;

namespace CoSpace.Core.Interface
{
    public interface IComplaintRepository
    {
        Task<Complaint> AddComplaint(Complaint Complaint);
        Task<bool> UpdateComplaint(Complaint Complaint);
        Task<bool> DeleteComplaint(Complaint complaint);
        Task<Complaint> GetComplaintById(int id);
        Task<IEnumerable<Complaint>> GetComplaints();
    }
}
