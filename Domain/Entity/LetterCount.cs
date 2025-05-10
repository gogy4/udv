namespace Domain.Model;

public class LetterCount
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public char Letter { get; set; }
    public int Count { get; set; }
}