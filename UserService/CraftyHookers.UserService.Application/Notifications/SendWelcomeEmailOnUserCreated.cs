using CraftyHookers.EmailService.Core;
using MediatR;

namespace CraftyHookers.UserService.Application.Notifications
{
    public class SendWelcomeEmailOnUserCreated(IEmailSender emailSender) : INotificationHandler<UserCreatedNotification>
    {
        public Task Handle(UserCreatedNotification notification, CancellationToken cancellationToken)
        {
            // User has no dedicated Email property yet - UserName is used as the recipient
            // as a placeholder until that's decided.
            var message = new EmailMessage(
                To: notification.User.Email,
                Subject: "Welcome - your temporary password",
                Body: $"Hi {notification.User.DisplayName}, your temporary password is: {notification.TemporaryPassword}");

            return emailSender.SendAsync(message, cancellationToken);
        }
    }
}
