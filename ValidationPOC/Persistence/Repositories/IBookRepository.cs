namespace ValidationPOC.Persistence.Repositories;

public interface IBookRepository
{
    Task<int> GetCountAsync(string authorId);

    Task<bool> ExistsAsync(string authorId, string title);
}