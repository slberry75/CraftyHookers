using CraftyHookers.UserService.Core.Entities;
using MediatR;

namespace CraftyHookers.UserService.Application.Notifications
{
    public record UserCreatedNotification(User User, string TemporaryPassword) : INotification;
}
