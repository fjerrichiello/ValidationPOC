using Microsoft.EntityFrameworkCore;
using ValidationPOC.Domain.Models;

namespace ValidationPOC.Persistence.Repositories;

public class BookCountRepository(ApplicationDbContext _context) : IBookCountRepository
{
    public async Task<BookCount?> GetBookCountForAuthor(string authorId)
    {
        var bookCount =
            await _context.BookCounts.FirstOrDefaultAsync(bc => bc.AuthorId.ToLower().Equals(authorId.ToLower()));
        return bookCount is null
            ? null
            : new BookCount(bookCount.MainId, bookCount.AuthorId, bookCount.Count);
    }

    public async Task AddAsync(string authorId)
        => await _context.BookCounts.AddAsync(new Models.BookCount
        {
            MainId = Guid.NewGuid(),
            AuthorId = authorId,
            Count = 1,
        });

    public async Task IncrementCountAsync(Guid id)
    {
        var bookCount = await _context.BookCounts.FindAsync(id);
        if (bookCount is null)
            throw new InvalidOperationException("Entity doesn't exist");

        bookCount.Count += 1;
    }
}