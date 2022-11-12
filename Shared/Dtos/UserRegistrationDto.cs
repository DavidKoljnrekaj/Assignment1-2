namespace Shared.Dtos;

public class UserRegistrationDTO
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string email { get; set; }
    
    
    public UserRegistrationDTO(string userName, string password, string email)
    {
        UserName = userName;
        Password = password;
        this.email = email;
    }
    
}