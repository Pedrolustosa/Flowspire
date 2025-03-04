using Flowspire.Application.DTOs;

namespace Flowspire.Application.Interfaces;
public interface IMessageService
{
    Task<MessageDTO> SendMessageAsync(MessageDTO messageDto);
    Task<List<MessageDTO>> GetMessagesAsync(string userId, string otherUserId);
}