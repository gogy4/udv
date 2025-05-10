namespace Service.Mapper;

public static class PostMapper
{
    public static Post ToEntity(PostDto dto, string userId)
    {
        return new Post
        {
            Text = dto.Text,
            UserId = userId
        };
    }

    public static PostDto ToDto(Post entity)
    {
        return new PostDto(entity.UserId, entity.Text);
    }
}