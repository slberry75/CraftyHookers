using CraftyHookers.UserService.Core.Entities;
using CraftyHookers.UserService.Core.Repositories;
using CraftyHookers.UserService.Core.Security;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CraftyHookers.UserService.Application.Commands
{
    public record LoginUserCommand(string Email, string Password) : IRequest<string?>;

    public class LoginUserCommandHandler(IUserRepository repository, IPasswordHasher passwordHasher) : IRequestHandler<LoginUserCommand, string?>
    {
        public async Task<string?> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await repository.GetUserByEmailAsync(request.Email);

            if (user != null && passwordHasher.Verify(request.Password, user.PasswordHash))
            {
                return "login-successful"; // Replace with actual token generation logic
            }
            
            return null; // Return null if login fails
        }
    }
}
