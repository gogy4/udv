using Domain.Model;
using Service.Dto;

namespace Service.Mapper;

public static class LetterCountMapper
{
    public static LetterCount ToEntity(LetterCountDto dto, string userId)
    {
        return new LetterCount
        {
            Count = dto.Count,
            Letter = dto.Letter,
            UserId = userId
        };
    }

    public static LetterCountDto ToDto(LetterCount entity)
    {
        return new LetterCountDto(entity.UserId, entity.Letter, entity.Count);
    }
}