using CraftyHookers.UserService.Core.Repositories;
using CraftyHookers.UserService.Core.Security;
using CraftyHookers.UserService.Infrastructure.Data;
using CraftyHookers.UserService.Infrastructure.Repository;
using CraftyHookers.UserService.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CraftyHookers.UserService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services)
        {
            services.AddDbContext<CraftyHookersDbContext>(options =>
            {
                options.UseSqlServer("Server=(local);Database=CraftyHookers;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true");
            });
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddSingleton<IPasswordHasher, PasswordService>();
            services.AddSingleton<IPasswordGenerator, PasswordService>();
            return services;
        }
    }
}
