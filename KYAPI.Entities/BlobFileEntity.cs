using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KYAPI.Enums;
using Microsoft.AspNetCore.Identity;

namespace KYAPI.Entities;

public class BlobFileEntity : BaseEntity
{
    [Required]
    public string FileName { get; set; } = string.Empty;

    [Required]
    public string SystemFileName { get; set; } = string.Empty;

    [Required]
    public string BlobType { get; set; } = string.Empty;

    [Required]
    public string BlobPath { get; set; } = string.Empty;

    public BlobContentType ContentType { get; set; }
}
