namespace ValidationPOC.Domain.Models;

public record EditBookRequest(string AuthorId, string Title, string NewTitle);