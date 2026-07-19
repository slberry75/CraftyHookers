using CraftyHookers.UserService.Core.Entities;
using CraftyHookers.UserService.Core.Repositories;
using MediatR;

namespace CraftyHookers.UserService.Application.Commands
{
    public record AddUserCommand(User user) : IRequest<User>;

    public class AddUserCommandHandler(IUserRepository repository)
        : IRequestHandler<AddUserCommand, User>
    {
        public async Task<User> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            return await repository.AddUserAsync(request.user);
        }
    }
}
