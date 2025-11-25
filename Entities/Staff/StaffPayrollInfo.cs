using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace KYAPI.Entities;

public class StaffPayrollInfo : BaseEntity
{
    #region Foreign key section
    public long StaffUserId { get; set; }

    [ForeignKey(nameof(StaffUserId))]
    [JsonIgnore]
    public virtual ApplicationUser? StaffUser { get; set; }
    #endregion

    [Required]
    [Precision(10, 2)]
    public decimal Salary { get; set; }

    [Required]
    [StringLength(50)]
    public string BankName { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string BankAccountNumber { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string EPFNo { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string SocsoNo { get; set; } = string.Empty;

    [StringLength(50)]
    public string? TaxNo { get; set; } = string.Empty;
}
