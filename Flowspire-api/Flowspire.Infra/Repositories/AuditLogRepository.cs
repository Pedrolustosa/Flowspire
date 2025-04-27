using Flowspire.Domain.Entities;
using Flowspire.Domain.Interfaces;
using Flowspire.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Flowspire.Infra.Repositories
{
    public class AuditLogRepository(ApplicationDbContext context) : IAuditLogRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task AddAsync(AuditLog auditLog)
        {
            await _context.AuditLogs.AddAsync(auditLog);
            await _context.SaveChangesAsync();
        }

        public async Task<List<AuditLog>> GetAllAsync()
        {
            return await _context.AuditLogs.OrderByDescending(x => x.Timestamp).ToListAsync();
        }

        public IQueryable<AuditLog> Query()
        {
            return _context.AuditLogs.AsQueryable();
        }

        public async Task DeleteLogsOlderThanAsync(DateTime cutoffDate)
        {
            var oldLogs = await _context.AuditLogs
                .Where(log => log.Timestamp < cutoffDate)
                .ToListAsync();

            if (oldLogs.Count!=0)
            {
                _context.AuditLogs.RemoveRange(oldLogs);
                await _context.SaveChangesAsync();
            }
        }

    }
}
