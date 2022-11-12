using Application.DaoInterfaces;
using Shared.Dtos;
using Shared.Model;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserDao userDao;

    public UserLogic(IUserDao userDao)
    {
        this.userDao = userDao;
    }

    async public Task<User> RegisterAsync(UserRegistrationDTO dto)
    {
        User? existing = await userDao.GetByUsernameAsync(dto.UserName);
        if (existing != null)
            throw new Exception("Username already taken!");

        ValidateData(dto);
        User toRegister = new User
        {
            Username = dto.UserName,
            Password = dto.Password,
            Email = dto.email,
            Role = "RegisteredUser"
        };
    
        User registered = await userDao.RegisterAsync(toRegister);
    
        return registered;
    }
    async public Task<User> ValidateUserAsync(string username, string password)
    {
        User? existingUser = await userDao.GetByUsernameAsync(username);
        
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return existingUser;
    }
    
    
    private static void ValidateData(UserRegistrationDTO dto)
    {
        string userName = dto.UserName;

        if (userName.Length < 3)
            throw new Exception("Username must be at least 3 characters!");

        if (userName.Length > 15)
            throw new Exception("Username must be less than 16 characters!");
        if (userName.Length < 6)
            throw new Exception("Password must be at least 6 characters!");

        if (userName.Length > 15)
            throw new Exception("Password must be less than 16 characters!");
    }
    
    
}