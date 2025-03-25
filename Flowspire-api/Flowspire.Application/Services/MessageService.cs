using Flowspire.Application.DTOs;
using Flowspire.Application.Interfaces;
using Flowspire.Domain.Entities;
using Flowspire.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flowspire.Application.Services;

public class MessageService(IMessageRepository messageRepository, INotificationService notificationService) : IMessageService
{
    private readonly IMessageRepository _messageRepository = messageRepository ?? throw new ArgumentNullException(nameof(messageRepository));
    private readonly INotificationService _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));

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

            await _notificationService.SendMessageAsync(addedMessage.ReceiverId, addedMessage);
            await _notificationService.SendNotificationAsync(addedMessage.ReceiverId, $"New message from {addedMessage.SenderId}");

            return messageToSend;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Error saving the message to the database.", ex);
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

    public async Task<MessageDTO> UpdateMessageAsync(int messageId, string newContent)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(newContent))
            {
                throw new ArgumentException("Message content cannot be empty.", nameof(newContent));
            }

            var message = await _messageRepository.GetByIdAsync(messageId);
            if (message == null)
            {
                throw new KeyNotFoundException("Message not found.");
            }

            if (message.IsRead)
            {
                throw new InvalidOperationException("Cannot edit a message that has already been read.");
            }

            message.EditContent(newContent);
            var updatedMessage = await _messageRepository.UpdateAsync(message);

            var messageDto = new MessageDTO
            {
                Id = updatedMessage.Id,
                SenderId = updatedMessage.SenderId,
                ReceiverId = updatedMessage.ReceiverId,
                Content = updatedMessage.Content,
                SentAt = updatedMessage.SentAt,
                IsRead = updatedMessage.IsRead,
                ReadAt = updatedMessage.ReadAt
            };

            await _notificationService.NotifyMessageUpdatedAsync(updatedMessage.ReceiverId, updatedMessage);

            return messageDto;
        }
        catch (KeyNotFoundException ex)
        {
            throw new KeyNotFoundException("Message not found.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Unexpected error while updating message.", ex);
        }
    }

    public async Task DeleteMessageAsync(int messageId)
    {
        try
        {
            var message = await _messageRepository.GetByIdAsync(messageId);
            if (message == null)
            {
                throw new KeyNotFoundException("Message not found.");
            }

            await _messageRepository.DeleteAsync(messageId);
            await _notificationService.NotifyMessageDeletedAsync(message.ReceiverId, messageId);
        }
        catch (KeyNotFoundException ex)
        {
            throw new KeyNotFoundException("Message not found.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Unexpected error while deleting message.", ex);
        }
    }

    public async Task MarkMessageAsReadAsync(int messageId)
    {
        try
        {
            var message = await _messageRepository.GetByIdAsync(messageId);
            if (message == null)
            {
                throw new KeyNotFoundException("Message not found.");
            }

            if (message.IsRead)
            {
                throw new InvalidOperationException("The message has already been marked as read.");
            }

            message.MarkAsRead();
            var updatedMessage = await _messageRepository.UpdateAsync(message);

            var messageDto = new MessageDTO
            {
                Id = updatedMessage.Id,
                SenderId = updatedMessage.SenderId,
                ReceiverId = updatedMessage.ReceiverId,
                Content = updatedMessage.Content,
                SentAt = updatedMessage.SentAt,
                IsRead = updatedMessage.IsRead,
                ReadAt = updatedMessage.ReadAt
            };

            await _notificationService.NotifyMessageReadAsync(updatedMessage.SenderId, updatedMessage);
        }
        catch (KeyNotFoundException ex)
        {
            throw new KeyNotFoundException("Message not found.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Unexpected error while marking message as read.", ex);
        }
    }

    public async Task<MessageDTO> GetByIdAsync(int messageId)
    {
        try
        {
            var message = await _messageRepository.GetByIdAsync(messageId);
            if (message == null)
            {
                throw new KeyNotFoundException("Message not found.");
            }

            var messageDto = new MessageDTO
            {
                Id = message.Id,
                SenderId = message.SenderId,
                ReceiverId = message.ReceiverId,
                Content = message.Content,
                SentAt = message.SentAt,
                IsRead = message.IsRead,
                ReadAt = message.ReadAt
            };

            return messageDto;
        }
        catch (KeyNotFoundException ex)
        {
            throw new KeyNotFoundException("Message not found.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Unexpected error while retrieving message.", ex);
        }
    }
}