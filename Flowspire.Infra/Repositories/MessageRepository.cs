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
        try
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return message;
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Erro ao adicionar a mensagem ao banco de dados.", ex);
        }
        catch (SqliteException ex)
        {
            throw new Exception("Erro de conexão ou operação no SQLite ao adicionar mensagem.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro inesperado ao adicionar mensagem.", ex);
        }
    }

    public async Task<List<Message>> GetMessagesBetweenUsersAsync(string userId1, string userId2)
    {
        try
        {
            return await _context.Messages
                .Where(m => (m.SenderId == userId1 && m.ReceiverId == userId2) ||
                            (m.SenderId == userId2 && m.ReceiverId == userId1))
                .OrderBy(m => m.SentAt)
                .ToListAsync();
        }
        catch (SqliteException ex)
        {
            throw new Exception("Erro de conexão ou operação no SQLite ao recuperar mensagens.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro inesperado ao recuperar mensagens.", ex);
        }
    }
}