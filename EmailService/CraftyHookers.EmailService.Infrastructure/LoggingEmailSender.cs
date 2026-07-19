using CraftyHookers.EmailService.Core;
using Microsoft.Extensions.Logging;

namespace CraftyHookers.EmailService.Infrastructure
{
    // Stand-in until a real sender (e.g. MailKit against smtp4dev/a provider) replaces it -
    // logs instead of sending, so callers can be built and tested against the contract now.
    public class LoggingEmailSender(ILogger<LoggingEmailSender> logger) : IEmailSender
    {
        public Task SendAsync(EmailMessage message, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Email sent to {To} with subject '{Subject}'", message.To, message.Subject);
            return Task.CompletedTask;
        }
    }
}
