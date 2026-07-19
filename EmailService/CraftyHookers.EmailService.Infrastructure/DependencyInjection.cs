using CraftyHookers.EmailService.Core;
using Microsoft.Extensions.DependencyInjection;

namespace CraftyHookers.EmailService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddEmailServiceInfrastructureDI(this IServiceCollection services)
        {
            services.AddSingleton<IEmailSender, LoggingEmailSender>();
            return services;
        }
    }
}
