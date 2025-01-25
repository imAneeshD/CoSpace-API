using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Commands.HelpCommands
{
    public record DeleteHelpRequestCommand(int id) : IRequest<bool>;

    public class DeleteHelpRequestCommandHandler(IHelpRepository helpRepository) : IRequestHandler<DeleteHelpRequestCommand, bool>
    {
        public async Task<bool> Handle(DeleteHelpRequestCommand request, CancellationToken cancellationToken)
        {
            return await helpRepository.DeleteHelpRequest(request.id);
        }
    }
}
