using Microsoft.AspNetCore.Identity;

namespace MyWebApi.Entities;

public class ApplicationUserClaim : IdentityUserClaim<long>
{
    // Add custom properties here if needed
}
