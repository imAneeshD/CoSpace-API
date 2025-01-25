using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Commands.HelpCommands
{
    public record UpdateHelpRequestCommand(HelpRequest HelpRequest) : IRequest<bool>;

    public class UpdateHelpRequestCommandHandler(IHelpRepository helpRepository) : IRequestHandler<UpdateHelpRequestCommand, bool>
    {
        public async Task<bool> Handle(UpdateHelpRequestCommand request, CancellationToken cancellationToken)
        {
            return await helpRepository.UpdateHelpRequest(request.HelpRequest);
        }
    }
}
