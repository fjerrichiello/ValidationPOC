namespace ValidationPOC.Domain.Models;

public record BookCount(Guid Id, string AuthorId, int Count)
{
};