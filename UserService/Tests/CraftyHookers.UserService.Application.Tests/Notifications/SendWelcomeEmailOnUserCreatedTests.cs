using CraftyHookers.EmailService.Core;
using CraftyHookers.UserService.Application.Notifications;
using CraftyHookers.UserService.Core.Entities;
using Moq;

namespace CraftyHookers.UserService.Application.Tests.Notifications
{
    public class SendWelcomeEmailOnUserCreatedTests
    {
        private readonly Mock<IEmailSender> _emailSender = new();
        private readonly SendWelcomeEmailOnUserCreated _sut;

        public SendWelcomeEmailOnUserCreatedTests()
        {
            _sut = new SendWelcomeEmailOnUserCreated(_emailSender.Object);
        }

        [Fact]
        public async Task Handle_SendsAnEmail_ContainingTheTemporaryPassword()
        {
            var user = new User { Email = "jdoe@example.com", DisplayName = "Jane Doe" };
            var notification = new UserCreatedNotification(user, "temp-password-123");

            await _sut.Handle(notification, CancellationToken.None);

            _emailSender.Verify(s => s.SendAsync(
                It.Is<EmailMessage>(m => m.To == "jdoe@example.com" && m.Body.Contains("temp-password-123")),
                It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}
