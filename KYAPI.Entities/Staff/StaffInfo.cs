using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KYAPI.Entities;

public class StaffInfo : BaseEntity
{
    #region Foreign key section
    public long StaffUserId { get; set; }

    [JsonIgnore]
    [ForeignKey(nameof(StaffUserId))]
    public virtual ApplicationUser? StaffUser { get; set; }
    #endregion

    [Required]
    [StringLength(200)]
    public string FirstName { get; set; } 

    [Required]
    [StringLength(200)]
    public string LastName { get; set; } 

    [Required]
    [StringLength(200)]
    public string NickName { get; set; }

    [Required]
    [StringLength(1)]
    public string Gender { get; set; } 

    [Required]
    [StringLength(5)]
    public string Nationality { get; set; } 

    [StringLength(12)]
    public string? IdentificationNo { get; set; } 

    [StringLength(50)]
    public string? PassportNo { get; set; } 

    [Required]
    [StringLength(200)]
    public string Email { get; set; } 

    [Required]
    [StringLength(11)]
    public string PhoneNumber { get; set; } 

    [Required]
    [StringLength(5)]
    public string EmploymentType { get; set; } 

    [Required]
    [StringLength(5)]
    public string EmploymentStatus { get; set; } 
    [Required]
    [StringLength(5)]
    public string Position { get; set; } 

    [Required]
    [StringLength(5)]
    public string Department { get; set; } = string.Empty;

    [Required]
    public DateTime DateJoined { get; set; } = DateTime.Now;

    public DateTime? DateResigned { get; set; }

    public DateTime? LastWorkingDate { get; set; }
}
