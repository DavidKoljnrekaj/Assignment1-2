using Shared.Dtos;
using Shared.Model;

namespace Application;

public interface IPostLogic
{
    Task<Post> CreateAsync(PostCreationDTO dto);
    Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters);
    Task<Post> getByIdAsync(int id);
    Task UpdateAsync(PostUpdateDto todo);
    Task DeleteAsync(int id);
}