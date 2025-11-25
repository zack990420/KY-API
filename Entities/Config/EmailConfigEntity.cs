using System.ComponentModel.DataAnnotations;

namespace KYAPI.Entities;

public class EmailConfigEntity : BaseEntity
{
    [Required]
    public string SmtpHost { get; set; } = string.Empty;

    public int SmtpPort { get; set; }

    [Required]
    public string SmtpUsername { get; set; } = string.Empty;

    [Required]
    public string SmtpPassword { get; set; } = string.Empty;

    [Required]
    public string FromEmail { get; set; } = string.Empty;

    [Required]
    public string FromName { get; set; } = string.Empty;

    public bool EnableSsl { get; set; } = true;

    public bool IsActive { get; set; } = true;
}
