using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Queries.ComplaintQueries
{
    public record GetComplaintByIdQuery(int id) : IRequest<Complaint>;

    public class GetComplaintByIdQueryQueryHandler(IComplaintRepository ComplaintRepository)
        : IRequestHandler<GetComplaintByIdQuery, Complaint>
    {
        public async Task<Complaint> Handle(GetComplaintByIdQuery request, CancellationToken cancellationToken)
        {
            return await ComplaintRepository.GetComplaintById(request.id);
        }
    }
}
