using Flowspire.Domain.Entities;
using Flowspire.Domain.Interfaces;
using Flowspire.Infra.Common;
using Flowspire.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Flowspire.Infra.Repositories;

public class AuditLogRepository(ApplicationDbContext context, ILogger<AuditLogRepository> logger) : IAuditLogRepository
{
    private readonly ApplicationDbContext _context = context;
    private readonly ILogger<AuditLogRepository> _logger = logger;

    public async Task AddAsync(AuditLog auditLog)
    {
        await RepositoryHelper.ExecuteAsync(async () =>
        {
            await _context.AuditLogs.AddAsync(auditLog);
            await _context.SaveChangesAsync();
        }, _logger, nameof(AddAsync));
    }

    public async Task<List<AuditLog>> GetAllAsync()
    {
        return await RepositoryHelper.ExecuteAsync(async () =>
        {
            return await _context.AuditLogs
                .OrderByDescending(x => x.Timestamp)
                .ToListAsync();
        }, _logger, nameof(GetAllAsync));
    }

    public IQueryable<AuditLog> Query()
    {
        return _context.AuditLogs.AsQueryable();
    }

    public async Task DeleteLogsOlderThanAsync(DateTime cutoffDate)
    {
        await RepositoryHelper.ExecuteAsync(async () =>
        {
            var oldLogs = await _context.AuditLogs
                .Where(log => log.Timestamp < cutoffDate)
                .ToListAsync();

            if (oldLogs.Any())
            {
                _context.AuditLogs.RemoveRange(oldLogs);
                await _context.SaveChangesAsync();
            }
        }, _logger, nameof(DeleteLogsOlderThanAsync));
    }
}
