using Infrastructure.Data;
using Infrastructure.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Implementation;

public class PostRepository(AppDbContext context) : IRepository<Post>
{
    public async Task<Post> GetByIdAsync(int id)
    {
        return await context.Posts.FindAsync(id);
    }

    public async Task DeleteAsync(Post post)
    {
        context.Posts.Remove(post);
        await context.SaveChangesAsync();
    }

    public async Task AddAsync(Post post)
    {
        await context.Posts.AddAsync(post);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Post post)
    { 
        context.Posts.Update(post);
        await context.SaveChangesAsync();
    }

    public async Task ClearAsync(string userId)
    {
        context.RemoveRange(context.Posts.Where(p=>p.UserId == userId));
        await context.SaveChangesAsync();
    }

    public async Task AddRangeAsync(IEnumerable<Post> posts)
    {
        await context.Posts.AddRangeAsync(posts);
        await context.SaveChangesAsync();
    }
}