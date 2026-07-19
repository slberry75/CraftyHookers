using CraftyHookers.UserService.Core.Entities;

namespace CraftyHookers.UserService.Core.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User?> GetUserByEmailAsync(string userName);
        Task<User?> UpdateUserAsync(Guid userId, User user);
        Task<User> AddUserAsync(User user);
        Task<bool> DeleteUserAsync(Guid userId);
    }
}