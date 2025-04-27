using Flowspire.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flowspire.Domain.Interfaces;

public interface IAuditLogRepository
{
    Task AddAsync(AuditLog auditLog);
    Task<List<AuditLog>> GetAllAsync();
    IQueryable<AuditLog> Query();
    Task DeleteLogsOlderThanAsync(DateTime cutoffDate);

}
