using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Flowspire.Application.Common;

public static class ServiceHelper
{
    public static async Task<T> ExecuteAsync<T>(
        Func<Task<T>> action,
        ILogger logger,
        string operationName)
    {
        try
        {
            var result = await action();
            logger.LogInformation("Operation {OperationName} completed successfully.", operationName);
            return result;
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database error during {OperationName}.", operationName);
            throw;
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Resource not found during {OperationName}.", operationName);
            throw;
        }
        catch (UnauthorizedAccessException ex)
        {
            logger.LogWarning(ex, "Unauthorized access during {OperationName}.", operationName);
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error during {OperationName}.", operationName);
            throw;
        }
    }

    public static async Task ExecuteAsync(
        Func<Task> action,
        ILogger logger,
        string operationName)
    {
        try
        {
            await action();
            logger.LogInformation("Operation {OperationName} completed successfully.", operationName);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database error during {OperationName}.", operationName);
            throw;
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Resource not found during {OperationName}.", operationName);
            throw;
        }
        catch (UnauthorizedAccessException ex)
        {
            logger.LogWarning(ex, "Unauthorized access during {OperationName}.", operationName);
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error during {OperationName}.", operationName);
            throw;
        }
    }
}
