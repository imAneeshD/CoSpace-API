using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Commands.ComplaintCommands
{
    public record DeleteComplaintCommand(int id) : IRequest<bool>;

    public class DeleteComplaintCommandHandler(IComplaintRepository ComplaintRepository) : IRequestHandler<DeleteComplaintCommand, bool>
    {
        public async Task<bool> Handle(DeleteComplaintCommand request, CancellationToken cancellationToken)
        {
            return await ComplaintRepository.DeleteComplaint(request.id);
        }
    }
}
