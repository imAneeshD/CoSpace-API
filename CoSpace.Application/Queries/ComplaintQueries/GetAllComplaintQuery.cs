using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Queries.ComplaintQueries
{
    public record GetAllComplaintQuery() : IRequest<IEnumerable<Complaint>>;

    public class GetAllComplaintQueryHandler(IComplaintRepository ComplaintRepository)
        : IRequestHandler<GetAllComplaintQuery, IEnumerable<Complaint>>
    {
        public async Task<IEnumerable<Complaint>> Handle(GetAllComplaintQuery request, CancellationToken cancellationToken)
        {
            return await ComplaintRepository.GetComplaints();
        }
    }
}
