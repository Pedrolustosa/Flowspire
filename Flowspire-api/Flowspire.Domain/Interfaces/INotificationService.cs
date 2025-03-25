using Flowspire.Domain.Entities;
using System.Threading.Tasks;

namespace Flowspire.Domain.Interfaces;

public interface INotificationService
{
    Task SendNotificationAsync(string userId, string message);
    Task SendMessageAsync(string receiverId, Message message);
    Task NotifyMessageUpdatedAsync(string receiverId, Message message);
    Task NotifyMessageDeletedAsync(string receiverId, int messageId);
    Task NotifyMessageReadAsync(string senderId, Message message);
}