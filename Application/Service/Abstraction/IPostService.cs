namespace Service.Service.Abstraction;

public interface IPostService
{
    Task<IEnumerable<PostDto>> GetLastPostAsync(string userId);
}