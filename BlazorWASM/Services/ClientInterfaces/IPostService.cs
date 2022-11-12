using Shared.Dtos;
using Shared.Model;

namespace BlazorWasm.Services;

public interface IPostService
{
    Task<ICollection<Post>> getAsync(string? title, string? username);
    Task<Post> getById(int id);
    Task<Post> postAsync(PostCreationDTO dto);
}