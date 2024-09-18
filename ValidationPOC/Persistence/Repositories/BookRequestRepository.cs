using Microsoft.EntityFrameworkCore;
using ValidationPOC.Domain.Models;
using ValidationPOC.Enums;

namespace ValidationPOC.Persistence.Repositories;

public class BookRequestRepository(ApplicationDbContext _context) : IBookRequestRepository
{
    public async Task<int> GetPendingAddRequestCountAsync(string authorId)
        => await _context.BookRequests.CountAsync(x =>
            x.AuthorId.ToLower().Equals(authorId.ToLower())
            && x.ApprovalStatus == ApprovalStatus.Pending);

    public async Task AddAddBookRequestAsync(AddBookRequest addBookRequest)
        => await _context.BookRequests.AddAsync(new Models.BookRequest()
        {
            MainId = Guid.NewGuid(),
            AuthorId = addBookRequest.AuthorId,
            Title = addBookRequest.Title,
            NewTitle = addBookRequest.Title,
            RequestType = RequestType.Add,
            ApprovalStatus = ApprovalStatus.Pending
        });

    public async Task AddEditBookRequestAsync(EditBookRequest editBookRequest)
        => await _context.BookRequests.AddAsync(new Models.BookRequest()
        {
            MainId = Guid.NewGuid(),
            AuthorId = editBookRequest.AuthorId,
            Title = editBookRequest.Title,
            NewTitle = editBookRequest.NewTitle,
            RequestType = RequestType.Edit,
            ApprovalStatus = ApprovalStatus.Pending
        });

    public async Task<bool> PendingExistsAsync(string authorId, string title)
        => await _context.BookRequests.AnyAsync(x =>
            x.AuthorId.ToLower().Equals(authorId.ToLower())
            && x.Title.ToLower().Equals(title.ToLower())
            && x.ApprovalStatus == ApprovalStatus.Pending);
}