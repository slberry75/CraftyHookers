using CraftyHookers.UserService.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CraftyHookers.UserService.Infrastructure.Data
{
    public class CraftyHookersDbContext(DbContextOptions<CraftyHookersDbContext> options) 
        : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            { 
                base.OnModelCreating(modelBuilder);
                modelBuilder.ApplyConfigurationsFromAssembly(typeof(CraftyHookersDbContext).Assembly);
            }
        }
    }
}
