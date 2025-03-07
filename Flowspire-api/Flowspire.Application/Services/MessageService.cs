using Microsoft.AspNetCore.SignalR;
using Flowspire.Application.Interfaces;
using Flowspire.Domain.Entities;
using Flowspire.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using Flowspire.Domain.Hubs;
using Flowspire.Domain.Interfaces;

namespace Flowspire.Application.Services;

public class MessageService(IMessageRepository messageRepository, 
                            IHubContext<NotificationHub> hubContext) : IMessageService
{
    private readonly IMessageRepository _messageRepository = messageRepository;
    private readonly IHubContext<NotificationHub> _hubContext = hubContext;

    public async Task<MessageDTO> SendMessageAsync(MessageDTO messageDto)
    {
        try
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
        catch (DbUpdateException ex)
        {
            throw new Exception("Erro ao salvar a mensagem no banco de dados.", ex);
        }
        catch (HubException ex)
        {
            throw new Exception("Erro ao enviar mensagem via SignalR.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro inesperado ao enviar mensagem.", ex);
        }
    }

    public async Task<List<MessageDTO>> GetMessagesAsync(string userId, string otherUserId)
    {
        try
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
        catch (Exception ex)
        {
            throw new Exception("Erro inesperado ao recuperar mensagens.", ex);
        }
    }
}