using Flowspire.Application.DTOs;
using Flowspire.Application.Interfaces;
using Flowspire.Domain.Interfaces;
using Flowspire.Application.Common;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Flowspire.Application.Services;

public class AuditLogService(
    IAuditLogRepository auditLogRepository,
    ILogger<AuditLogService> logger) : IAuditLogService
{
    private readonly IAuditLogRepository _auditLogRepository = auditLogRepository
            ?? throw new ArgumentNullException(nameof(auditLogRepository));
    private readonly ILogger<AuditLogService> _logger = logger
            ?? throw new ArgumentNullException(nameof(logger));

    public async Task<PagedResult<AuditLogDTO>> GetAuditLogsAsync(
        PaginationQuery paginationQuery,
        CancellationToken cancellationToken = default)
    {
        if (paginationQuery == null)
            throw new ArgumentNullException(nameof(paginationQuery));

        return await ServiceHelper.ExecuteAsync(async () =>
        {
            cancellationToken.ThrowIfCancellationRequested();
            _logger.LogInformation("Retrieving audit logs: Page={Page}, Size={Size}",
                paginationQuery.Page, paginationQuery.PageSize);

            var query = _auditLogRepository.Query();
            var totalCount = await query.CountAsync(cancellationToken);

            var logs = await query
                .OrderByDescending(x => x.Timestamp)
                .Skip((paginationQuery.Page - 1) * paginationQuery.PageSize)
                .Take(paginationQuery.PageSize)
                .Select(log => new AuditLogDTO
                {
                    Id = log.Id,
                    UserId = log.UserId,
                    IpAddress = log.IpAddress,
                    Method = log.Method,
                    Path = log.Path,
                    StatusCode = log.StatusCode,
                    ExecutionTimeMs = log.ExecutionTimeMs,
                    Timestamp = log.Timestamp
                })
                .ToListAsync(cancellationToken);

            if (!logs.Any())
                throw new KeyNotFoundException(ErrorMessages.NoAuditLogsFound);

            _logger.LogInformation("Retrieved {Count} logs out of {Total}.", logs.Count, totalCount);

            return new PagedResult<AuditLogDTO>
            {
                Data = logs,
                TotalCount = totalCount
            };
        }, _logger, nameof(GetAuditLogsAsync));
    }

    public async Task CleanupOldLogsAsync(
        CancellationToken cancellationToken = default)
    {
        await ServiceHelper.ExecuteAsync(async () =>
        {
            cancellationToken.ThrowIfCancellationRequested();
            var cutoffDate = DateTime.UtcNow.AddDays(-30);

            _logger.LogInformation("Deleting audit logs older than {Cutoff}.", cutoffDate);
            await _auditLogRepository.DeleteLogsOlderThanAsync(cutoffDate);
            _logger.LogInformation("Audit logs cleanup completed.");
        }, _logger, nameof(CleanupOldLogsAsync));
    }
}
