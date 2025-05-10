using Service.Dto;

namespace Service.Service.Abstraction;

public interface ILetterCountService
{
    public Task<IEnumerable<LetterCountDto>> CountSharedLetterOccurrencesAsync(string userId, IEnumerable<PostDto> posts);
}