using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Flowspire.Domain.Entities;
using Flowspire.Infra.Data;
using Flowspire.Domain.Interfaces;
using Flowspire.Infra.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flowspire.Infra.Repositories;

public class MessageRepository(ApplicationDbContext context, ILogger<MessageRepository> logger) : IMessageRepository
{
    private readonly ApplicationDbContext _context = context;
    private readonly ILogger<MessageRepository> _logger = logger;

    public async Task<Message> AddAsync(Message message)
    {
        return await RepositoryHelper.ExecuteAsync(async () =>
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return message;
        }, _logger, nameof(AddAsync));
    }

    public async Task<List<Message>> GetMessagesBetweenUsersAsync(string userId, string otherUserId)
    {
        return await RepositoryHelper.ExecuteAsync(async () =>
        {
            return await _context.Messages
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .Where(m =>
                    (m.SenderId == userId && m.ReceiverId == otherUserId) ||
                    (m.SenderId == otherUserId && m.ReceiverId == userId))
                .OrderBy(m => m.SentAt)
                .ToListAsync();
        }, _logger, nameof(GetMessagesBetweenUsersAsync));
    }
}
