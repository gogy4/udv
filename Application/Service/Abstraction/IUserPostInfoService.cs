using Service.Dto;

namespace Service.Service.Abstraction;

public interface IUserPostInfoService
{
    Task<UserPostInfoDto> GetUserPostInfoAsync(string userId);
}