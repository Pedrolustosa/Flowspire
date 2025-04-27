namespace Flowspire.Application.Common;

public static class ErrorMessages
{
    public const string UserNotFound = "User not found.";
    public const string InvalidCredentials = "Invalid credentials.";
    public const string UnauthorizedAccess = "Unauthorized access.";
    public const string RoleNotFound = "Role not found.";
    public const string RoleAlreadyAssigned = "User already has the specified role.";
    public const string RoleNotAssigned = "User does not have the specified role.";
    public const string RefreshTokenInvalidOrExpired = "Invalid or expired refresh token.";
    public const string ValidationFailed = "Validation failed.";
    public const string UnexpectedError = "An unexpected error occurred.";
    public const string DatabaseSaveError = "Error saving data to the database.";
    public const string DatabaseUpdateError = "Error updating data in the database.";
    public const string NoAuditLogsFound = "No audit logs were found.";
}