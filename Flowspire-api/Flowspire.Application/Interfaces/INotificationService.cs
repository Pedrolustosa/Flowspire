using Flowspire.Application.DTOs;
using System.Threading.Tasks;

namespace Flowspire.Application.Interfaces;

public interface INotificationService
{
    Task SendMessageAsync(string senderId, string receiverId, MessageDTO message);
}