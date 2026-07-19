using CraftyHookers.UserService.Core.Entities;
using CraftyHookers.UserService.Core.Repositories;
using CraftyHookers.UserService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CraftyHookers.UserService.Infrastructure.Repository
{
    public class UserRepository(CraftyHookersDbContext context) : IUserRepository
    {
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<User?> GetUserByEmailAsync(string userName)
        {
            return await context.Users.Where(u => u.Email == userName).FirstOrDefaultAsync();
        }

        public async Task<User?> UpdateUserAsync(Guid userId, User user)
        {
            var u = await context.Users.Where(u => u.UserId == userId).FirstOrDefaultAsync();
            if (u != null)
            {
                u.Email = user.Email;
                await context.SaveChangesAsync();
            }
            return u;
        }

        public async Task<User> AddUserAsync(User user)
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            var u = await context.Users.Where(u => u.UserId == userId).FirstOrDefaultAsync();
            if (u != null)
            {
                context.Users.Remove(u);
                return await context.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
