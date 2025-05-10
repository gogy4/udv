namespace Service.Dto;

public class LetterCountDto(string userId, char letter, int count)
{
    public char Letter { get; set; } = letter;
    public int Count { get; set; } = count;
}