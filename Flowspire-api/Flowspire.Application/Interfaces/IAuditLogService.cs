using Flowspire.Application.Common;
using Flowspire.Application.DTOs;

namespace Flowspire.Application.Interfaces;

public interface IAuditLogService
{
    Task<PagedResult<AuditLogDTO>> GetAuditLogsAsync(
        PaginationQuery paginationQuery,
        CancellationToken cancellationToken = default);

    Task CleanupOldLogsAsync(
        CancellationToken cancellationToken = default);
}
