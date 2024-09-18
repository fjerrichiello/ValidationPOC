using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ValidationPOC.Enums;

namespace ValidationPOC.Persistence.Models;

public class BookRequest
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid MainId { get; init; }

    public required string AuthorId { get; set; }

    public required string Title { get; set; }
    
    public required string NewTitle { get; set; }

    public RequestType RequestType { get; set; }

    public ApprovalStatus ApprovalStatus { get; set; }

    public ICollection<BookRequestDeclineReason>? DeclineReasons { get; set; } = [];

    [Timestamp]
    public uint? Version { get; set; }
}