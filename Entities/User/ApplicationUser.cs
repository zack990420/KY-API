using Microsoft.AspNetCore.Identity;

namespace KYAPI.Entities;

public class ApplicationUser : IdentityUser<long>
{
    // Add custom properties here
    // Example:
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    
    // Refresh Token
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
}
