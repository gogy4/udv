using Domain.Model;

namespace Infrastructure.Repository.Abstraction;

public interface IRepository<T> where T : class
{
    public Task<T> GetByIdAsync(int id);
    public Task DeleteAsync(T letterCount);
    public Task AddAsync(T letterCount);
    public Task UpdateAsync(T letterCount);
    public Task ClearAsync(string userId);
    public Task AddRangeAsync(IEnumerable<T> letterCounts);
}