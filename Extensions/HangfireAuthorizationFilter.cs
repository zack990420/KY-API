using Hangfire.Dashboard;

namespace KYAPI.Extensions;

public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        var httpContext = context.GetHttpContext();

        // Check if user is authenticated
        if (!httpContext.User.Identity?.IsAuthenticated ?? true)
            return false;

        // Check if user has SuperAdmin role
        return httpContext.User.IsInRole("SuperAdmin");
    }
}
