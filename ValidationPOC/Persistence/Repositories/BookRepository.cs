using Microsoft.EntityFrameworkCore;

namespace ValidationPOC.Persistence.Repositories;

public class BookRepository(ApplicationDbContext _context) : IBookRepository
{
    public async Task<int> GetCountAsync(string authorId)
        => await _context.Books.CountAsync(x => x.AuthorId.ToLower().Equals(authorId.ToLower()));

    public async Task<bool> ExistsAsync(string authorId, string title)
        => await _context.Books.AnyAsync(x =>
            x.AuthorId.ToLower().Equals(authorId.ToLower())
            && x.Title.ToLower().Equals(title.ToLower()));
}