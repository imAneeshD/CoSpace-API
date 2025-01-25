using CoSpace.Core.Entities;
using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Commands.CanteenCommands
{
    public record AddCanteenMenuCommand(CanteenMenu CanteenMenu) : IRequest<CanteenMenu>;

    public class AddCanteenMenuCommandHandler(ICanteenRepository canteenRepository) 
        : IRequestHandler<AddCanteenMenuCommand, CanteenMenu>
    {
        public async Task<CanteenMenu> Handle(AddCanteenMenuCommand request, CancellationToken cancellationToken)
        {
            return await canteenRepository.AddCanteenMenu(request.CanteenMenu);
        }
    }
}
