using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Dtos;
using Shared.Model;

namespace EfcDataAccess.DAOs;

public class PostEfcDAO : IPostDao
{
    private readonly PostContext context;

    public PostEfcDAO(PostContext context)
    {
        this.context = context;
    }
    public async Task<Post> CreateAsync(Post post)
    {
        EntityEntry<Post> added = await context.Posts.AddAsync(post);
        await context.SaveChangesAsync();
        return added.Entity;
    }
    public async Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
    {
        IQueryable<Post> result = context.Posts.Include(post => post.user).AsQueryable();

        if (!string.IsNullOrEmpty(searchParameters.Username))
        {
            result = result.Where(post =>
                post.user.Username.ToLower().Equals(searchParameters.Username.ToLower()));
        }
        
        
        if (!string.IsNullOrEmpty(searchParameters.TitleContains))
        {
            result = result.Where(t =>
                t.title.ToLower().Contains(searchParameters.TitleContains.ToLower()));
        }

        List<Post> david = await result.ToListAsync();
        return david;
    }
    public Task<Post> GetByIdAsync(int id)
    {
        Post? existing = context.Posts.FirstOrDefault(p => p.postId == id);
        return Task.FromResult(existing);
    }
    public Task UpdateAsync(Post post)
    {
        throw new NotImplementedException();
    }
    public async Task DeleteAsync(int id)
    {
        Post? existing = await GetByIdAsync(id);
        if (existing == null)
        {
            throw new Exception($"Post with id {id} not found");
        }

        context.Posts.Remove(existing);
        await context.SaveChangesAsync();
    }
}