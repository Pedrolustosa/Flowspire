using Flowspire.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flowspire.Application.Interfaces;

public interface IMessageService
{
    Task<MessageDTO> SendMessageAsync(MessageDTO messageDto);
    Task<List<MessageDTO>> GetMessagesAsync(string userId, string otherUserId);
}