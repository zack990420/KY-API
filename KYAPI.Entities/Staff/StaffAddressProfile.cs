using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KYAPI.Entities;

public class StaffAddressProfile : BaseEntity
{
    #region Foreign key section
    public long StaffUserId { get; set; }

    [ForeignKey(nameof(StaffUserId))]
    [JsonIgnore]
    public virtual ApplicationUser? StaffUser { get; set; }
    #endregion

    [Required]
    [StringLength(200)]
    public string AddressLine1 { get; set; } 

    [StringLength(200)]
    public string? AddressLine2 { get; set; } 

    [Required]
    [StringLength(100)]
    public string City { get; set; } 

    [Required]
    [StringLength(100)]
    public string State { get; set; } 

    [Required]
    [StringLength(100)]
    public string Country { get; set; } 

    [Required]
    [StringLength(10)]
    public string ZipCode { get; set; } 
}