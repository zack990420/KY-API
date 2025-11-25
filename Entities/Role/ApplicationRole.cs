using Microsoft.AspNetCore.Identity;

namespace MyWebApi.Entities;

public class ApplicationRole : IdentityRole<long>
{
    // Add custom properties here
    // Example:
    // public string? Description { get; set; }
    // public DateTime CreatedAt { get; set; }
}
