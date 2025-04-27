using Flowspire.Application.Common;
using Flowspire.Application.DTOs;

namespace Flowspire.Application.Interfaces
{
    public interface IAuditLogService
    {
        Task<PagedResult<AuditLogDTO>> GetAuditLogsAsync(PaginationQuery paginationQuery);
        Task CleanupOldLogsAsync();
    }
}
