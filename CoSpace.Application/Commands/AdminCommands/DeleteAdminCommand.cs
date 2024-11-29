using CoSpace.Core.Interface;
using MediatR;

namespace CoSpace.Application.Commands.AdminCommands
{
    public record DeleteAdminCommand(int Id) : IRequest<bool>;

    public class DeleteAdminCommandHandler(IAdminRepository adminsRepository)
        : IRequestHandler<DeleteAdminCommand, bool>
    {
        public async Task<bool> Handle(DeleteAdminCommand request, CancellationToken cancellationToken)
        {
            return await adminsRepository.DeleteAdmin(request.Id);
        }
    }
}
