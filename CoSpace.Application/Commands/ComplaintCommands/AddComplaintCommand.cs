using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Commands.ComplaintCommands
{
    public record AddComplaintCommand(Complaint Complaint) : IRequest<Complaint>;

    public class AddComplaintCommandHandler(IComplaintRepository ComplaintRepository) 
        : IRequestHandler<AddComplaintCommand, Complaint>
    {
        public async Task<Complaint> Handle(AddComplaintCommand request, CancellationToken cancellationToken)
        {
            return await ComplaintRepository.AddComplaint(request.Complaint);
        }
    }
}
