using System.ComponentModel.DataAnnotations;

namespace Shared.Model;

public class Post
{
    public string title { get; set; }
    [Key]
    public int postId { get; set; }
    public string content { get; set;}
    public User user { get;  set; }
    public Post( User user,string title, string content)
    {
        this.title = title;
        this.content = content;
        this.user = user;
        postId = 0;
    }
    public Post()
    {
    }
    
    
    
}