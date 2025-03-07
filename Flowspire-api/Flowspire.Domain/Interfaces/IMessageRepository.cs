using Flowspire.Domain.Entities;

namespace Flowspire.Domain.Interfaces
{
    public interface IMessageRepository
    {
        Task<Message> AddAsync(Message message);
        Task<List<Message>> GetMessagesBetweenUsersAsync(string userId1, string userId2);
    }
}
