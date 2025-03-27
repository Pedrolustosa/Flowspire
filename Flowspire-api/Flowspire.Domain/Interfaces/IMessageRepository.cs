using Flowspire.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flowspire.Domain.Interfaces;

public interface IMessageRepository
{
    Task<Message> AddAsync(Message message);
    Task<List<Message>> GetMessagesBetweenUsersAsync(string userId, string otherUserId);
}