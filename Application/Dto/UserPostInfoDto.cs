namespace Service.Dto;

public class UserPostInfoDto(string userId, IEnumerable<PostDto> posts, IEnumerable<LetterCountDto> letterCounts)
{
    public string UserId { get; set; } = userId;
    public IEnumerable<PostDto> Posts { get; set; } = posts;
    public IEnumerable<LetterCountDto> LetterCounts { get; set; } = letterCounts;
}