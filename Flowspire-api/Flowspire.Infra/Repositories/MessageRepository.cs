using Microsoft.EntityFrameworkCore;
using Flowspire.Domain.Entities;
using Flowspire.Infra.Data;
using Flowspire.Domain.Interfaces;
using Microsoft.Data.Sqlite;

namespace Flowspire.Infra.Repositories;

public class MessageRepository(ApplicationDbContext context) : IMessageRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Message> AddAsync(Message message)
    {
        _context.Messages.Add(message);
        await _context.SaveChangesAsync();
        return message;
    }

    public async Task<Message> UpdateAsync(Message message)
    {
        var existingMessage = await _context.Messages.FindAsync(message.Id);
        if (existingMessage == null)
        {
            throw new KeyNotFoundException($"Message with ID {message.Id} not found.");
        }

        existingMessage.MarkAsRead();
        existingMessage.EditContent(message.Content);
        await _context.SaveChangesAsync();
        return existingMessage;
    }

    public async Task DeleteAsync(int messageId)
    {
        var message = await _context.Messages.FindAsync(messageId);
        if (message == null)
        {
            throw new KeyNotFoundException($"Message with ID {messageId} not found.");
        }

        _context.Messages.Remove(message);
        await _context.SaveChangesAsync();
    }

    public async Task<Message> GetByIdAsync(int messageId)
    {
        var message = await _context.Messages
            .Include(m => m.Sender)
            .Include(m => m.Receiver)
            .FirstOrDefaultAsync(m => m.Id == messageId);
        if (message == null)
        {
            throw new KeyNotFoundException($"Message with ID {messageId} not found.");
        }
        return message;
    }

    public async Task<List<Message>> GetMessagesBetweenUsersAsync(string userId, string otherUserId)
    {
        return await _context.Messages
            .Include(m => m.Sender)
            .Include(m => m.Receiver)
            .Where(m => (m.SenderId == userId && m.ReceiverId == otherUserId) ||
                        (m.SenderId == otherUserId && m.ReceiverId == userId))
            .OrderBy(m => m.SentAt)
            .ToListAsync();
    }
}