namespace CraftyHookers.UserService.Core.Security
{
    public interface IPasswordGenerator
    {
        string Generate(int length = 12);
    }
}
