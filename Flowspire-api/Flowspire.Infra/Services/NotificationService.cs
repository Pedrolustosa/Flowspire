using Microsoft.AspNetCore.SignalR;
using Flowspire.Application.Interfaces;
using Flowspire.Application.DTOs;
using Flowspire.Domain.Hubs;
using Microsoft.Extensions.Logging;
using Flowspire.Domain.Interfaces;
using Flowspire.Domain.Entities;

namespace Flowspire.Infra.Services;

public class NotificationService(IHubContext<NotificationHub> hubContext, ILogger<NotificationService> logger) : INotificationService
{
    private readonly IHubContext<NotificationHub> _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
    private readonly ILogger<NotificationService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task SendNotificationAsync(string userId, string message)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            _logger.LogError("Attempt to send notification with null or empty userId.");
            throw new ArgumentException("The userId cannot be null or empty.", nameof(userId));
        }

        if (string.IsNullOrWhiteSpace(message))
        {
            _logger.LogError("Attempt to send notification with null or empty message to user {UserId}.", userId);
            throw new ArgumentException("The message cannot be null or empty.", nameof(message));
        }

        try
        {
            _logger.LogInformation("Sending notification to user {UserId}: {Message}", userId, message);
            await _hubContext.Clients.User(userId).SendAsync("ReceiveNotification", message);
            _logger.LogInformation("Notification sent successfully to user {UserId}.", userId);
        }
        catch (HubException ex)
        {
            _logger.LogError(ex, "Error sending notification via SignalR to user {UserId}.", userId);
            throw new InvalidOperationException("Error sending notification via SignalR.", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while sending notification to user {UserId}.", userId);
            throw new InvalidOperationException("Unexpected error while sending notification.", ex);
        }
    }

    public async Task SendMessageAsync(string receiverId, Message message)
    {
        if (string.IsNullOrWhiteSpace(receiverId))
        {
            _logger.LogError("Attempt to send message with null or empty receiverId.");
            throw new ArgumentException("The receiverId cannot be null or empty.", nameof(receiverId));
        }

        if (message == null)
        {
            _logger.LogError("Attempt to send null message to user {ReceiverId}.", receiverId);
            throw new ArgumentNullException(nameof(message));
        }

        try
        {
            _logger.LogInformation("Sending message via SignalR to user {ReceiverId}. MessageId: {MessageId}", receiverId, message.Id);
            await _hubContext.Clients.User(receiverId).SendAsync("ReceiveMessage", message);
            _logger.LogInformation("Message sent successfully via SignalR to user {ReceiverId}. MessageId: {MessageId}", receiverId, message.Id);
        }
        catch (HubException ex)
        {
            _logger.LogError(ex, "Error sending message via SignalR to user {ReceiverId}. MessageId: {MessageId}", receiverId, message.Id);
            throw new InvalidOperationException("Error sending message via SignalR.", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while sending message to user {ReceiverId}. MessageId: {MessageId}", receiverId, message.Id);
            throw new InvalidOperationException("Unexpected error while sending message.", ex);
        }
    }

    public async Task NotifyMessageUpdatedAsync(string receiverId, Message message)
    {
        if (string.IsNullOrWhiteSpace(receiverId))
        {
            _logger.LogError("Attempt to notify message update with null or empty receiverId.");
            throw new ArgumentException("The receiverId cannot be null or empty.", nameof(receiverId));
        }

        if (message == null)
        {
            _logger.LogError("Attempt to notify update of a null message to user {ReceiverId}.", receiverId);
            throw new ArgumentNullException(nameof(message));
        }

        try
        {
            _logger.LogInformation("Notifying message update via SignalR to user {ReceiverId}. MessageId: {MessageId}", receiverId, message.Id);
            await _hubContext.Clients.User(receiverId).SendAsync("MessageUpdated", message);
            _logger.LogInformation("Message update notification sent successfully to user {ReceiverId}. MessageId: {MessageId}", receiverId, message.Id);
        }
        catch (HubException ex)
        {
            _logger.LogError(ex, "Error notifying message update via SignalR to user {ReceiverId}. MessageId: {MessageId}", receiverId, message.Id);
            throw new InvalidOperationException("Error notifying message update via SignalR.", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while notifying message update to user {ReceiverId}. MessageId: {MessageId}", receiverId, message.Id);
            throw new InvalidOperationException("Unexpected error while notifying message update.", ex);
        }
    }

    public async Task NotifyMessageDeletedAsync(string receiverId, int messageId)
    {
        if (string.IsNullOrWhiteSpace(receiverId))
        {
            _logger.LogError("Attempt to notify message deletion with null or empty receiverId.");
            throw new ArgumentException("The receiverId cannot be null or empty.", nameof(receiverId));
        }

        try
        {
            _logger.LogInformation("Notifying message deletion via SignalR to user {ReceiverId}. MessageId: {MessageId}", receiverId, messageId);
            await _hubContext.Clients.User(receiverId).SendAsync("MessageDeleted", messageId);
            _logger.LogInformation("Message deletion notification sent successfully to user {ReceiverId}. MessageId: {MessageId}", receiverId, messageId);
        }
        catch (HubException ex)
        {
            _logger.LogError(ex, "Error notifying message deletion via SignalR to user {ReceiverId}. MessageId: {MessageId}", receiverId, messageId);
            throw new InvalidOperationException("Error notifying message deletion via SignalR.", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while notifying message deletion to user {ReceiverId}. MessageId: {MessageId}", receiverId, messageId);
            throw new InvalidOperationException("Unexpected error while notifying message deletion.", ex);
        }
    }

    public async Task NotifyMessageReadAsync(string senderId, Message message)
    {
        if (string.IsNullOrWhiteSpace(senderId))
        {
            _logger.LogError("Attempt to notify message read with null or empty senderId.");
            throw new ArgumentException("The senderId cannot be null or empty.", nameof(senderId));
        }

        if (message == null)
        {
            _logger.LogError("Attempt to notify read of a null message to user {SenderId}.", senderId);
            throw new ArgumentNullException(nameof(message));
        }

        try
        {
            _logger.LogInformation("Notifying message read via SignalR to user {SenderId}. MessageId: {MessageId}", senderId, message.Id);
            await _hubContext.Clients.User(senderId).SendAsync("MessageRead", message);
            _logger.LogInformation("Message read notification sent successfully to user {SenderId}. MessageId: {MessageId}", senderId, message.Id);
        }
        catch (HubException ex)
        {
            _logger.LogError(ex, "Error notifying message read via SignalR to user {SenderId}. MessageId: {MessageId}", senderId, message.Id);
            throw new InvalidOperationException("Error notifying message read via SignalR.", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while notifying message read to user {SenderId}. MessageId: {MessageId}", senderId, message.Id);
            throw new InvalidOperationException("Unexpected error while notifying message read.", ex);
        }
    }
}