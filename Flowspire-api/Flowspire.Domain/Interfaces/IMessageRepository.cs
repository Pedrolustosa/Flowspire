using Flowspire.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flowspire.Domain.Interfaces;

public interface IMessageRepository
{
    Task<Message> AddAsync(Message message);
    Task<Message> UpdateAsync(Message message);
    Task DeleteAsync(int messageId);
    Task<Message> GetByIdAsync(int messageId);
    Task<List<Message>> GetMessagesBetweenUsersAsync(string userId, string otherUserId);
}