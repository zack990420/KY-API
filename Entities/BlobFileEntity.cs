using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using KYAPI.Enums;

namespace KYAPI.Entities;

public class BlobFileEntity
{
    [Key]
    public long Id { get; set; }

    [Required]
    public string FileName { get; set; } = string.Empty;

    [Required]
    public string SystemFileName { get; set; } = string.Empty;

    [Required]
    public string BlobType { get; set; } = string.Empty;

    [Required]
    public string BlobPath { get; set; } = string.Empty;

    public BlobContentType ContentType { get; set; }

    public long EntryBy { get; set; }
    
    
    [ForeignKey(nameof(EntryBy))]
    public ApplicationUser? User { get; set; }

    public DateTime EntryOn { get; set; } = DateTime.UtcNow;
}
