using Hangfire.Dashboard;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Flowspire.API.Filters;

using Hangfire.Dashboard;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public class HangfireJwtAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        var httpContext = context.GetHttpContext();
        var token = httpContext.Request.Cookies["X-Access-Token"];

        if (string.IsNullOrEmpty(token))
            return false;

        var handler = new JwtSecurityTokenHandler();
        try
        {
            var jwtToken = handler.ReadJwtToken(token);

            var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role || c.Type == "role");
            var isAdmin = roleClaim != null && roleClaim.Value == "Administrator";

            return isAdmin;
        }
        catch
        {
            return false;
        }
    }
}
