using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Flowspire.Infra.Common;

public static class RepositoryHelper
{
    public static async Task<T> ExecuteAsync<T>(Func<Task<T>> action, ILogger logger, string operationName)
    {
        try
        {
            var result = await action();
            logger.LogInformation("Repository operation {OperationName} completed successfully.", operationName);
            return result;
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update error during {OperationName}.", operationName);
            throw new Exception($"Database update error during {operationName}.", ex);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected repository error during {OperationName}.");
            throw new Exception($"Unexpected repository error during {operationName}.", ex);
        }
    }

    public static async Task ExecuteAsync(Func<Task> action, ILogger logger, string operationName)
    {
        try
        {
            await action();
            logger.LogInformation("Repository operation {OperationName} completed successfully.", operationName);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update error during {OperationName}.", operationName);
            throw new Exception($"Database update error during {operationName}.", ex);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected repository error during {OperationName}.");
            throw new Exception($"Unexpected repository error during {operationName}.", ex);
        }
    }
}
