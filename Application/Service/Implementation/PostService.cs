using System.Text.Json;
using Infrastructure.Repository.Abstraction;
using Microsoft.Extensions.Configuration;
using Service.Mapper;
using Service.Service.Abstraction;

namespace Service.Service.Implementation;

public class PostService(HttpClient httpClient, IRepository<Post> repository, IConfiguration configuration)
    : IPostService
{
    public async Task<IEnumerable<PostDto>> GetLastPostAsync(string userId)
    {
        var url =
            $"https://api.vk.com/method/wall.get?domain={userId}&count=5&access_token=" +
            $"{configuration["VkApi:AccessToken"]}&v=5.131";
        var response = await httpClient.GetStringAsync(url);
        var json = JsonDocument.Parse(response);
        var root = json.RootElement;
        if (root.TryGetProperty("error", out var error))
        {
            var errorMessage = error.GetProperty("error_msg").GetString();
            throw new Exception(errorMessage);
        }

        var items = json.RootElement
            .GetProperty("response")
            .GetProperty("items")
            .EnumerateArray()
            .Select(item => new PostDto(userId, item.GetProperty("text").GetString().ToLower()));
        await repository.ClearAsync(userId);
        await repository.AddRangeAsync(items.Select(x => PostMapper.ToEntity(x, userId)));
        return items;
    }
}