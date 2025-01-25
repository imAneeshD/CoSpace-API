using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Commands.CanteenCommands
{
    public record DeleteCanteenMenuCommand(int id) : IRequest<bool>;

    public class DeleteCanteenMenuCommandHandler(ICanteenRepository canteenRepository) : IRequestHandler<DeleteCanteenMenuCommand, bool>
    {
        public async Task<bool> Handle(DeleteCanteenMenuCommand request, CancellationToken cancellationToken)
        {
            return await canteenRepository.DeleteCanteenMenu(request.id);
        }
    }
}
