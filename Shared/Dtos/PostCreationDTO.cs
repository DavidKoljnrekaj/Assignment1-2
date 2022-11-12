namespace Shared.Dtos;

public class PostCreationDTO
{
    public string Username { get; }
    public string Title { get; }
    public string Content { get; }
    
    
    public PostCreationDTO(string username, string title, string content)
        {
            Username = username;
            Title = title;
            Content = content;
        }
}