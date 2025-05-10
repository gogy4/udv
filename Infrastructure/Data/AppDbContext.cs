using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options)
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<LetterCount> LetterCounts { get; set; }
}