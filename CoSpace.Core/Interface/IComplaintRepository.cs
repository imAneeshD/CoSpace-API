using CoSpace.Core.Entities;

namespace CoSpace.Core.Interface
{
    public interface IComplaintRepository
    {
        Task<Complaint> AddComplaint(Complaint Complaint);
        Task<bool> UpdateComplaint(Complaint Complaint);
        Task<bool> DeleteComplaint(int id);
        Task<Complaint> GetComplaintById(int id);
        Task<IEnumerable<Complaint>> GetComplaints();
    }
}
