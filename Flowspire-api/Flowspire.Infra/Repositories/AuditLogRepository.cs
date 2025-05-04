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

    public Task AddAsync(AuditLog log)
            => RepositoryHelper.ExecuteAsync(
                async () => { await _context.AuditLogs.AddAsync(log); await _context.SaveChangesAsync(); },
                _logger,
                nameof(AddAsync));

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

    public Task DeleteLogsOlderThanAsync(DateTime threshold)
            => RepositoryHelper.ExecuteAsync(
                async () =>
                {
                    var old = _context.AuditLogs.Where(l => l.Timestamp < threshold);
                    _context.AuditLogs.RemoveRange(old);
                    await _context.SaveChangesAsync();
                },
                _logger,
                nameof(DeleteLogsOlderThanAsync));
}
