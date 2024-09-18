using System.ComponentModel.DataAnnotations;

namespace ValidationPOC.Persistence.Models;

public class Author
{
    [Key]
    public int Id { get; set; }

    public required string AuthorId { get; set; }
}