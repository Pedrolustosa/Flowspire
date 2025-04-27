using Flowspire.Application.DTOs;
using Flowspire.Application.Interfaces;
using Flowspire.Domain.Interfaces;
using Flowspire.Application.Common;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Flowspire.Application.Services
{
    public class AuditLogService(IAuditLogRepository auditLogRepository, ILogger<AuditLogService> logger) : IAuditLogService
    {
        private readonly IAuditLogRepository _auditLogRepository = auditLogRepository;
        private readonly ILogger<AuditLogService> _logger = logger;

        public async Task<PagedResult<AuditLogDTO>> GetAuditLogsAsync(PaginationQuery paginationQuery)
        {
            return await ServiceHelper.ExecuteAsync(async () =>
            {
                var query = _auditLogRepository.Query();

                var totalCount = await query.CountAsync();

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
                    .ToListAsync();

                return new PagedResult<AuditLogDTO>
                {
                    Data = logs,
                    TotalCount = totalCount
                };
            }, _logger, nameof(GetAuditLogsAsync));
        }

        public async Task CleanupOldLogsAsync()
        {
            await ServiceHelper.ExecuteAsync(async () =>
            {
                var cutoffDate = DateTime.UtcNow.AddDays(-30);

                await _auditLogRepository.DeleteLogsOlderThanAsync(cutoffDate);

                _logger.LogInformation("Audit logs older than {CutoffDate} have been deleted.", cutoffDate);
            }, _logger, nameof(CleanupOldLogsAsync));
        }

    }
}
