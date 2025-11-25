using System.ComponentModel.DataAnnotations;

namespace KYAPI.DTOs;

public class CreateStaffRequestDto
{
    // User Account Info
    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    // Staff Personal Info
    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    public string NickName { get; set; } = string.Empty;

    [Required]
    public string Gender { get; set; } = string.Empty; // M/F

    [Required]
    public string Nationality { get; set; } = string.Empty;

    public string? IdentificationNo { get; set; }
    public string? PassportNo { get; set; }

    [Required]
    public string PhoneNumber { get; set; } = string.Empty;

    // Employment Info
    [Required]
    public string EmploymentType { get; set; } = string.Empty; // FT/PT/CNT

    [Required]
    public string EmploymentStatus { get; set; } = string.Empty; // ACT/RSG

    [Required]
    public string Position { get; set; } = string.Empty;

    [Required]
    public string Department { get; set; } = string.Empty;

    public DateTime? DateJoined { get; set; }

    // Address Info
    [Required]
    public string AddressLine1 { get; set; } = string.Empty;

    public string? AddressLine2 { get; set; }

    [Required]
    public string City { get; set; } = string.Empty;

    [Required]
    public string State { get; set; } = string.Empty;

    [Required]
    public string Country { get; set; } = string.Empty;

    [Required]
    public string ZipCode { get; set; } = string.Empty;

    // Payroll Info
    [Required]
    public decimal Salary { get; set; }

    [Required]
    public string BankName { get; set; } = string.Empty;

    [Required]
    public string BankAccountNumber { get; set; } = string.Empty;

    [Required]
    public string EPFNo { get; set; } = string.Empty;

    [Required]
    public string SocsoNo { get; set; } = string.Empty;

    public string? TaxNo { get; set; }

    // Emergency Contact Info
    [Required]
    public string EmergencyContactName { get; set; } = string.Empty;

    [Required]
    public string EmergencyContactRelationship { get; set; } = string.Empty;

    [Required]
    public string EmergencyContactPhoneNumber { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string EmergencyContactEmail { get; set; } = string.Empty;
}
