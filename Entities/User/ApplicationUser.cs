using Microsoft.AspNetCore.Identity;

namespace KYAPI.Entities;

public class ApplicationUser : IdentityUser<long>
{
    // Refresh Token
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }

    public virtual StaffInfo? StaffInfo { get; set; }

    public virtual StaffAddressProfile? StaffAddressProfile { get; set; }

    public virtual StaffPayrollInfo? StaffPayrollInfo { get; set; }

    public ICollection<StaffEmergencyContact>? StaffEmergencyContact { get; set; }
}
