using Microsoft.EntityFrameworkCore;
using Flowspire.Domain.Entities;
using Flowspire.Infra.Data;
using Flowspire.Domain.Interfaces;
using Flowspire.Infra.Common;
using Microsoft.Extensions.Logging;
using System;

namespace Flowspire.Infra.Repositories;

public class MessageRepository(ApplicationDbContext context, ILogger<MessageRepository> logger) : IMessageRepository
{
    private readonly ApplicationDbContext _context = context;
    private readonly ILogger<MessageRepository> _logger = logger;

    public Task<Message> AddAsync(Message message)
        => RepositoryHelper.ExecuteAsync(
            async () => { await _context.Messages.AddAsync(message); await _context.SaveChangesAsync(); return message; },
            _logger,
            nameof(AddAsync));

    public Task<List<Message>> GetMessagesBetweenUsersAsync(string userId, string otherUserId)
        => RepositoryHelper.ExecuteAsync(
            () => _context.Messages
                .Where(m => (m.SenderId == userId && m.ReceiverId == otherUserId) ||
                            (m.SenderId == otherUserId && m.ReceiverId == userId))
                .OrderBy(m => m.SentAt)
                .ToListAsync(),
            _logger,
            nameof(GetMessagesBetweenUsersAsync));
}
