using System.Security.Claims;

namespace TradeWise.Extensions
{
    public static class ClaimsExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
            // Try multiple possible claim types
            var username = user?.FindFirst(ClaimTypes.GivenName)?.Value   // Standard claim type
                       ?? user?.FindFirst("given_name")?.Value            // JWT common claim
                       ?? user?.FindFirst("givenname")?.Value             // Alternative spelling
                       ?? user?.FindFirst(ClaimTypes.Name)?.Value         // Fallback to standard name
                       ?? user?.Identity?.Name;                           // Last resort

            if (string.IsNullOrEmpty(username))
            {
                throw new InvalidOperationException("Could not find username in any of the possible claims");
            }

            return username;
        }
    }
}