using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Service.Service.Abstraction;

namespace Service.Service.Implementation;

public class UserIdService(HttpClient httpClient, IConfiguration configuration) : IUserIdService
{
    public async Task<long> GetUserIdByScreenNameAsync(string screenName)
    {
        var url = $"https://api.vk.com/method/utils.resolveScreenName?screen_name={screenName}" +
                  $"&access_token={configuration["VkApi:AccessToken"]}&v=5.131";
        var response = await httpClient.GetStringAsync(url);
        var json = JsonDocument.Parse(response);
        var root = json.RootElement;

        if (!root.TryGetProperty("response", out var responseElement))
            throw new Exception("Unexpected response format.");
        if (responseElement.ValueKind == JsonValueKind.Array)
            throw new Exception("User not found");

        if (responseElement.ValueKind != JsonValueKind.Object) throw new Exception("Unexpected response format.");
        var objectId = responseElement.GetProperty("object_id").GetInt64();
        return objectId;
    }
}