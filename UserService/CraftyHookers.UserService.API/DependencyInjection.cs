using CraftyHookers.EmailService.Infrastructure;
using CraftyHookers.UserService.Application;
using CraftyHookers.UserService.Infrastructure;

namespace CraftyHookers.UserService.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddHookerApiDI(this IServiceCollection services)
        {
            services.AddApplicationDI()
                .AddInfrastructureDI()
                .AddEmailServiceInfrastructureDI();

            return services;
        }
    }
}
