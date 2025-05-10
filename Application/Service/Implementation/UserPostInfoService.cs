using Service.Dto;
using Service.Service.Abstraction;

namespace Service.Service.Implementation;

public class UserPostInfoService(
    IPostService postService,
    ILetterCountService letterCountService)
    : IUserPostInfoService
{
    public async Task<UserPostInfoDto> GetUserPostInfoAsync(string userId)
    {
        var posts = await postService.GetLastPostAsync(userId);
        var letterCounts = await letterCountService.CountSharedLetterOccurrencesAsync(userId, posts);
        return new UserPostInfoDto(userId, posts, letterCounts);
    }
}