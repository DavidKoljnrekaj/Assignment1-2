namespace Shared.Dtos;

public class PostUpdateDto
{
    public int Id { get; }
    public string? Title { get; set; }
    public string content { get; set; }

    public PostUpdateDto(int id)
    {
        Id = id;
    }
}