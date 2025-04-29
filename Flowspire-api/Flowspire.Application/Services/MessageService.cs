using Flowspire.Application.DTOs;
using Flowspire.Application.Interfaces;
using Flowspire.Application.Common;
using Flowspire.Domain.Entities;
using Flowspire.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Flowspire.Application.Services;

public class MessageService(
    IMessageRepository messageRepository,
    INotificationService notificationService,
    ILogger<MessageService> logger) : IMessageService
{
    private readonly IMessageRepository _messageRepository = messageRepository;
    private readonly INotificationService _notificationService = notificationService;
    private readonly ILogger<MessageService> _logger = logger;

    public async Task<MessageDTO> SendMessageAsync(MessageDTO messageDto)
    {
        return await ServiceHelper.ExecuteAsync(async () =>
        {
            if (string.IsNullOrWhiteSpace(messageDto.SenderId) ||
                string.IsNullOrWhiteSpace(messageDto.ReceiverId) ||
                string.IsNullOrWhiteSpace(messageDto.Content))
            {
                throw new ArgumentException(ErrorMessages.InvalidMessageData);
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

            await _notificationService.SendMessageAsync(messageToSend.SenderId, messageToSend.ReceiverId, messageToSend);

            return messageToSend;
        }, _logger, nameof(SendMessageAsync));
    }

    public async Task<List<MessageDTO>> GetMessagesAsync(string userId, string otherUserId)
    {
        return await ServiceHelper.ExecuteAsync(async () =>
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(otherUserId))
            {
                throw new ArgumentException(ErrorMessages.InvalidUserId);
            }

            var messages = await _messageRepository.GetMessagesBetweenUsersAsync(userId, otherUserId);

            return messages.Select(m => new MessageDTO
            {
                Id = m.Id,
                SenderId = m.SenderId,
                ReceiverId = m.ReceiverId,
                Content = m.Content,
                SentAt = m.SentAt,
                IsRead = m.IsRead,
                ReadAt = m.ReadAt
            }).ToList();
        }, _logger, nameof(GetMessagesAsync));
    }
}
