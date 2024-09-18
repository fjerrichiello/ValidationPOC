using ValidationPOC.Domain.Models;

namespace ValidationPOC.Persistence.Repositories;

public interface IBookRequestRepository
{
    Task<int> GetPendingAddRequestCountAsync(string authorId);
    Task AddAddBookRequestAsync(AddBookRequest addBookRequest);
    Task AddEditBookRequestAsync(EditBookRequest editBookRequest);

    Task<bool> PendingExistsAsync(string authorId, string title);
}