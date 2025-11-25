using Microsoft.AspNetCore.Identity;

namespace MyWebApi.Entities;

public class ApplicationUserToken : IdentityUserToken<long>
{
    // Add custom properties here if needed
}
