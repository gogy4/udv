using Domain.Model;
using Infrastructure.Repository.Abstraction;
using Microsoft.Extensions.Logging;
using Service.Dto;
using Service.Mapper;
using Service.Service.Abstraction;

namespace Service.Service.Implementation;

public class LetterCountService(IRepository<LetterCount> repository, ILogger<LetterCountService> logger) : ILetterCountService
{
    public async Task<IEnumerable<LetterCountDto>> CountSharedLetterOccurrencesAsync(string userId,
        IEnumerable<PostDto> posts)
    {
        logger.LogInformation("Начат подсчет букв для пользователя {UserId} в {StartTime}", userId, DateTime.UtcNow);

        var letters = posts
            .SelectMany(post => post.Text.Where(char.IsLetter))
            .GroupBy(letter => letter)
            .OrderBy(group => group.Key)
            .Select(group => new LetterCountDto(userId, group.Key, group.Count()));
        
        await repository.ClearAsync(userId);

        await repository
            .AddRangeAsync(letters.Select(x => LetterCountMapper.ToEntity(x, userId)));
        logger.LogInformation("Завершен подсчет букв для пользователя {UserId} в {EndTime}. Обработано {PostCount} постов.",
            userId, DateTime.UtcNow, posts.Count());;
        return letters;
    }
}