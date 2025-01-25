using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Commands.ComplaintCommands
{
    public record AddCComplaintCommand(Complaint Complaint) : IRequest<Complaint>;

    public class AddComplaintCommandHandler(IComplaintRepository ComplaintRepository) 
        : IRequestHandler<AddCComplaintCommand, Complaint>
    {
        public async Task<Complaint> Handle(AddCComplaintCommand request, CancellationToken cancellationToken)
        {
            return await ComplaintRepository.AddComplaint(request.Complaint);
        }
    }
}
