using Application.DaoInterfaces;
using Shared.Dtos;
using Shared.Model;

namespace Application.Logic;

public class PostLogic : IPostLogic
{
    private readonly IPostDao postDao;
    private readonly IUserDao userDao;

    public PostLogic(IPostDao postDao, IUserDao userDao)
    {
        this.postDao = postDao;
        this.userDao = userDao;
    }

    public async Task<Post> CreateAsync(PostCreationDTO dto)
    {
        User? user = await userDao.GetByUsernameAsync(dto.Username);
        if (user == null)
        {
            throw new Exception($"User {dto.Username} was not found.");
        }

        ValidatePost(dto);
        Post post = new Post(user,dto.Title,dto.Content);
        Post created = await postDao.CreateAsync(post);
        return created;
    }
    private void ValidatePost(PostCreationDTO dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
        if (string.IsNullOrEmpty(dto.Content)) throw new Exception("Post cannot be empty.");
    }
    private void ValidatePost(Post post)
    {
        if (string.IsNullOrEmpty(post.title)) throw new Exception("Title cannot be empty.");
        if (string.IsNullOrEmpty(post.content)) throw new Exception("Post cannot be empty.");
    }
    public Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
    {
        return postDao.GetAsync(searchParameters);
    }

    public async Task DeleteAsync(int id)
    {
       await postDao.DeleteAsync(id);
    }
    
    public async Task UpdateAsync(PostUpdateDto dto)
    {
        Post? existing = await postDao.GetByIdAsync(dto.Id);

        if (existing == null)
        {
            throw new Exception($"Post with ID {dto.Id} not found!");
        }
        
        
        string titleToUse = dto.Title ?? existing.title;
        string contentToUse = dto.content ?? existing.content;
    
        Post updated = new (existing.user,titleToUse,contentToUse)
        {
            postId = existing.postId
        };

        ValidatePost(updated);

        await postDao.UpdateAsync(updated);
    }

    public async Task<Post> getByIdAsync(int id)
    {
        Post? existing = await postDao.GetByIdAsync(id);

        if (existing == null)
        {
            throw new Exception($"Post with ID {id} not found!");
        }

        return existing;
    }
}