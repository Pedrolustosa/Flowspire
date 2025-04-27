using System.Security.Claims;

namespace Flowspire.API.Common;

public static class ClaimsPrincipalExtensions
{
    public static string GetUserId(this ClaimsPrincipal user)
    {
        if (user == null)
            throw new UnauthorizedAccessException("User context is missing.");

        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException("User ID not found in token.");

        return userId;
    }

    public static string GetUserName(this ClaimsPrincipal user)
    {
        return GetClaimValueOrDefault(user, ClaimTypes.Name);
    }

    public static string GetUserEmail(this ClaimsPrincipal user)
    {
        return GetClaimValueOrDefault(user, ClaimTypes.Email);
    }

    public static string GetUserRole(this ClaimsPrincipal user)
    {
        return GetClaimValueOrDefault(user, ClaimTypes.Role);
    }

    public static string GetUserCountry(this ClaimsPrincipal user)
    {
        return GetClaimValueOrDefault(user, ClaimTypes.Country);
    }

    public static string GetUserCity(this ClaimsPrincipal user)
    {
        return GetClaimValueOrDefault(user, ClaimTypes.Locality);
    }

    public static string GetUserPhoneNumber(this ClaimsPrincipal user)
    {
        return GetClaimValueOrDefault(user, ClaimTypes.MobilePhone);
    }

    public static string GetUserStreetAddress(this ClaimsPrincipal user)
    {
        return GetClaimValueOrDefault(user, ClaimTypes.StreetAddress);
    }

    public static string GetUserPostalCode(this ClaimsPrincipal user)
    {
        return GetClaimValueOrDefault(user, ClaimTypes.PostalCode);
    }

    public static string GetUserBirthDate(this ClaimsPrincipal user)
    {
        return GetClaimValueOrDefault(user, ClaimTypes.DateOfBirth);
    }

    public static string GetAuthenticationMethod(this ClaimsPrincipal user)
    {
        return GetClaimValueOrDefault(user, ClaimTypes.AuthenticationMethod);
    }

    private static string GetClaimValueOrDefault(ClaimsPrincipal user, string claimType)
    {
        if (user == null)
            return "Unknown";

        var value = user.FindFirst(claimType)?.Value;
        return string.IsNullOrEmpty(value) ? "Unknown" : value;
    }
}