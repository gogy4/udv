using Domain.Model;
using Infrastructure.Data;
using Infrastructure.Repository.Abstraction;

namespace Infrastructure.Repository.Implementation;

public class LetterCountRepository(AppDbContext context) : IRepository<LetterCount>
{
    public async Task<LetterCount> GetByIdAsync(int id)
    {
        return await context.LetterCounts.FindAsync(id);
    }

    public async Task DeleteAsync(LetterCount letterCount)
    {
        context.LetterCounts.Remove(letterCount);
        await context.SaveChangesAsync();
    }

    public async Task AddAsync(LetterCount letterCount)
    {
        await context.LetterCounts.AddAsync(letterCount);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(LetterCount letterCount)
    {
        context.LetterCounts.Update(letterCount);
        await context.SaveChangesAsync();
    }

    public async Task ClearAsync(string userId)
    {
        context.RemoveRange(context.LetterCounts.Where(l => l.UserId == userId));
        await context.SaveChangesAsync();
    }

    public async Task AddRangeAsync(IEnumerable<LetterCount> letterCounts)
    {
        await context.LetterCounts.AddRangeAsync(letterCounts);
        await context.SaveChangesAsync();
    }
}