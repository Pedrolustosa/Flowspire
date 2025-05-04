using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Flowspire.Infra.Common;

// Base para erros de repositório
public class RepositoryException(string operationName, string message, Exception inner) : Exception(message, inner)
{
    public string OperationName { get; } = operationName;
}

// Erros de update/insert/delete
public class RepositoryUpdateException(string operationName, Exception inner) : RepositoryException(operationName, $"Database update error during {operationName}.", inner)
{ }

// Erros de concorrência
public class RepositoryConcurrencyException(string operationName, Exception inner) : RepositoryException(operationName, $"Concurrency error during {operationName}.", inner)
{ }

public static class RepositoryHelper
{
    public static Task<T> ExecuteAsync<T>(
        Func<Task<T>> action,
        ILogger logger,
        string operationName)
        => ExecuteInternal(
            async () =>
            {
                var result = await action();
                logger.LogInformation("{OperationName} completed successfully.", operationName);
                return result;
            },
            logger,
            operationName);

    public static Task ExecuteAsync(
        Func<Task> action,
        ILogger logger,
        string operationName)
        => ExecuteInternal<bool>(
            async () =>
            {
                await action();
                logger.LogInformation("{OperationName} completed successfully.", operationName);
                return true;
            },
            logger,
            operationName);

    private static async Task<T> ExecuteInternal<T>(
        Func<Task<T>> action,
        ILogger logger,
        string operationName)
    {
        if (action == null) throw new ArgumentNullException(nameof(action));
        if (logger  == null) throw new ArgumentNullException(nameof(logger));
        if (string.IsNullOrWhiteSpace(operationName))
            throw new ArgumentException("Operation name must be provided.", nameof(operationName));

        try
        {
            return await action();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            logger.LogError(ex, "Concurrency error during {OperationName}.", operationName);
            throw new RepositoryConcurrencyException(operationName, ex);
        }
        catch (DbUpdateException ex)
        {
            logger.LogError(ex, "Database update error during {OperationName}.", operationName);
            throw new RepositoryUpdateException(operationName, ex);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected repository error during {OperationName}.", operationName);
            throw new RepositoryException(operationName, $"Unexpected error during {operationName}.", ex);
        }
    }
}
