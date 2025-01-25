using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Commands.ComplaintCommands
{
    public record UpdateComplaintCommand(Complaint Complaint) : IRequest<bool>;

    public class UpdateComplaintCommandHandler(IComplaintRepository ComplaintRepository) : IRequestHandler<UpdateComplaintCommand, bool>
    {
        public async Task<bool> Handle(UpdateComplaintCommand request, CancellationToken cancellationToken)
        {
            return await ComplaintRepository.UpdateComplaint(request.Complaint);
        }
    }
}
