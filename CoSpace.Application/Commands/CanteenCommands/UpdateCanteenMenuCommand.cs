using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Commands.CanteenCommands
{
    public record UpdateCanteenMenuCommand(CanteenMenu CanteenMenu) : IRequest<bool>;

    public class UpdateCanteenMenuCommandHandler(ICanteenRepository canteenRepository) : IRequestHandler<UpdateCanteenMenuCommand, bool>
    {
        public async Task<bool> Handle(UpdateCanteenMenuCommand request, CancellationToken cancellationToken)
        {
            return await canteenRepository.UpdateCanteenMenu(request.CanteenMenu);
        }
    }
}
