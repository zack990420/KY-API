using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KYAPI.Entities;

public class StaffEmergencyContact : BaseEntity
{
    #region Foreign key section
    public long StaffUserId { get; set; }

    [ForeignKey(nameof(StaffUserId))]
    [JsonIgnore]
    public virtual ApplicationUser? StaffUser { get; set; }
    #endregion

    [Required]
    [StringLength(200)]
    public string Name { get; set; }

    [Required]
    [StringLength(200)]
    public string Relationship { get; set; } 

    [Required]
    [StringLength(11)]
    public string PhoneNumber { get; set; } 

    [Required]
    [StringLength(200)]
    public string Email { get; set; } 
}
