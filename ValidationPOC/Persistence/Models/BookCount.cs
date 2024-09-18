using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ValidationPOC.Persistence.Models;

[Index(nameof(AuthorId), IsUnique = true)]
public class BookCount
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid MainId { get; init; }

    public required string AuthorId { get; set; }

    public required int Count { get; set; }

    [Timestamp]
    public uint? Version { get; set; }
}