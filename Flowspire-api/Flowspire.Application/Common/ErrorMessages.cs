namespace Flowspire.Application.Common;

public static class ErrorMessages
{
    // General
    public const string UnexpectedError = "An unexpected error occurred.";

    // Dashboard
    public const string InvalidTransactionType = "Type must be 'Expense' or 'Revenue'.";
    public const string InvalidTransactionLimit = "Limit must be between 1 and 50.";

    // User
    public const string UserNotFound = "User not found.";
    public const string UserAlreadyExists = "A user with this email already exists.";
    public const string UserNotIdentified = "User not identified.";
    public const string RoleNotFound = "Role not found.";
    public const string InvalidCredentials = "Invalid email or password.";
    public const string UnauthorizedRoleAssignment = "Unauthorized to assign this role.";
    public const string UnauthorizedAction = "You are not authorized to perform this action.";

    // Advisor
    public const string ForbiddenAccess = "You do not have permission to access this resource.";

    // Budget
    public const string BudgetNotFound = "Budget not found.";
    public const string BudgetIdMismatch = "The provided IDs do not match.";
    public const string InvalidModelState = "Invalid data submitted.";

    // Audit Log
    public const string AuditLogNotFound = "Audit log not found.";
    public const string NoAuditLogsFound = "Nenhum log de auditoria encontrado.";
    public const string AuditLogRetrievalFailed = "Failed to retrieve audit logs.";

    // Category
    public const string CategoryNotFound = "Category not found.";
    public const string IdMismatch = "The provided ID does not match the entity ID.";

    // Transaction
    public const string TransactionNotFound = "Transaction not found.";
    public const string TransactionIdMismatch = "Transaction ID mismatch.";

    // Message (Chat)
    public const string InvalidMessageData = "ReceiverId and Content are required.";
    public const string InvalidUserId = "OtherUserId is required.";
    public const string UserNotAuthenticated = "User not authenticated.";
}