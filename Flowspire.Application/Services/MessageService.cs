using Microsoft.AspNetCore.SignalR;
using Flowspire.Application.Interfaces;
using Flowspire.Domain.Entities;
using Flowspire.Application.DTOs;
using Flowspire.Domain.Interfaces;
using Flowspire.Domain.Hubs;

namespace Flowspire.Application.Services;
public class MessageService : IMessageService
{
    private readonly IMessageRepository _messageRepository;
    private readonly IHubContext<NotificationHub> _hubContext;

    public MessageService(IMessageRepository messageRepository, IHubContext<NotificationHub> hubContext)
    {
        _messageRepository = messageRepository;
        _hubContext = hubContext;
    }

    public async Task<MessageDTO> SendMessageAsync(MessageDTO messageDto)
    {
        var message = Message.Create(messageDto.SenderId, messageDto.ReceiverId, messageDto.Content);
        var addedMessage = await _messageRepository.AddAsync(message);

        await _hubContext.Clients.User(messageDto.ReceiverId)
            .SendAsync("ReceiveMessage", new MessageDTO
            {
                Id = addedMessage.Id,
                SenderId = addedMessage.SenderId,
                ReceiverId = addedMessage.ReceiverId,
                Content = addedMessage.Content,
                SentAt = addedMessage.SentAt,
                IsRead = addedMessage.IsRead
            });

        return new MessageDTO
        {
            Id = addedMessage.Id,
            SenderId = addedMessage.SenderId,
            ReceiverId = addedMessage.ReceiverId,
            Content = addedMessage.Content,
            SentAt = addedMessage.SentAt,
            IsRead = addedMessage.IsRead
        };
    }

    public async Task<List<MessageDTO>> GetMessagesAsync(string userId, string otherUserId)
    {
        var messages = await _messageRepository.GetMessagesBetweenUsersAsync(userId, otherUserId);
        return messages.Select(m => new MessageDTO
        {
            Id = m.Id,
            SenderId = m.SenderId,
            ReceiverId = m.ReceiverId,
            Content = m.Content,
            SentAt = m.SentAt,
            IsRead = m.IsRead
        }).ToList();
    }
}