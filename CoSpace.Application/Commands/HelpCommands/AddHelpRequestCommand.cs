using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Commands.HelpCommands
{
    public record AddHelpRequestCommand(HelpRequest HelpRequest) : IRequest<HelpRequest>;

    public class AddHelpRequestCommandHandler(IHelpRepository helpRepository) 
        : IRequestHandler<AddHelpRequestCommand, HelpRequest>
    {
        public async Task<HelpRequest> Handle(AddHelpRequestCommand request, CancellationToken cancellationToken)
        {
            return await helpRepository.AddHelpRequest(request.HelpRequest);
        }
    }
}
