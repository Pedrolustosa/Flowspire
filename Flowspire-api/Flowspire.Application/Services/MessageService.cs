using Flowspire.Application.DTOs;
using Flowspire.Application.Interfaces;
using Flowspire.Domain.Entities;
using Flowspire.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flowspire.Application.Services;

public class MessageService : IMessageService
{
    private readonly IMessageRepository _messageRepository;
    private readonly INotificationService _notificationService;

    public MessageService(IMessageRepository messageRepository, INotificationService notificationService)
    {
        _messageRepository = messageRepository ?? throw new ArgumentNullException(nameof(messageRepository));
        _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
    }

    public async Task<MessageDTO> SendMessageAsync(MessageDTO messageDto)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(messageDto.SenderId) || string.IsNullOrWhiteSpace(messageDto.ReceiverId) || string.IsNullOrWhiteSpace(messageDto.Content))
            {
                throw new ArgumentException("SenderId, ReceiverId, and Content are required.");
            }

            var message = Message.Create(messageDto.SenderId, messageDto.ReceiverId, messageDto.Content);
            var addedMessage = await _messageRepository.AddAsync(message);

            var messageToSend = new MessageDTO
            {
                Id = addedMessage.Id,
                SenderId = addedMessage.SenderId,
                ReceiverId = addedMessage.ReceiverId,
                Content = addedMessage.Content,
                SentAt = addedMessage.SentAt,
                IsRead = addedMessage.IsRead,
                ReadAt = addedMessage.ReadAt
            };

            // Notificar os usuários em tempo real
            await _notificationService.SendMessageAsync(messageToSend.SenderId, messageToSend.ReceiverId, messageToSend);

            return messageToSend;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Unexpected error while sending message.", ex);
        }
    }

    public async Task<List<MessageDTO>> GetMessagesAsync(string userId, string otherUserId)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(otherUserId))
            {
                throw new ArgumentException("userId and otherUserId are required.");
            }

            var messages = await _messageRepository.GetMessagesBetweenUsersAsync(userId, otherUserId);
            var messageDtos = messages.Select(m => new MessageDTO
            {
                Id = m.Id,
                SenderId = m.SenderId,
                ReceiverId = m.ReceiverId,
                Content = m.Content,
                SentAt = m.SentAt,
                IsRead = m.IsRead,
                ReadAt = m.ReadAt
            }).ToList();

            return messageDtos;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Unexpected error while retrieving messages.", ex);
        }
    }
}