using Shared.Dtos;
using Shared.Model;

namespace Application;

public interface IUserLogic
{
    Task<User> RegisterAsync(UserRegistrationDTO userToRegister);
    Task<User> ValidateUserAsync(string username, string password);
}