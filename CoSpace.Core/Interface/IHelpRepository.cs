using CoSpace.Core.Entities;

namespace CoSpace.Core.Interface
{
    public interface IHelpRepository
    {
        Task<HelpRequest> AddHelpRequest(HelpRequest HelpRequest);
        Task<bool> UpdateHelpRequest(HelpRequest HelpRequest);
        Task<bool> DeleteHelpRequest(int id);
        Task<HelpRequest> GetHelpRequestById(int id);
        Task<IEnumerable<HelpRequest>> GetHelpRequests();
    }
}
