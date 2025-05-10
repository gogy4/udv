namespace Service.Service.Abstraction;

public interface IUserIdService
{
    Task<long> GetUserIdByScreenNameAsync(string screenName);
}