using Flowspire.Application.DTOs;
using Flowspire.Application.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Flowspire.Domain.Hubs;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Flowspire.Infra.Services
{
    public class NotificationService : INotificationService
    {
        private const string ReceiveMessageMethod = "ReceiveMessage";
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(
            IHubContext<NotificationHub> hubContext,
            ILogger<NotificationService> logger)
        {
            _hubContext = hubContext
                ?? throw new ArgumentNullException(nameof(hubContext));
            _logger = logger
                ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task SendMessageAsync(
            string senderId,
            string receiverId,
            MessageDTO message,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(senderId))
                throw new ArgumentException("SenderId is required.", nameof(senderId));
            if (string.IsNullOrWhiteSpace(receiverId))
                throw new ArgumentException("ReceiverId is required.", nameof(receiverId));
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            try
            {
                _logger.LogInformation(
                    "Enviando mensagem de {Sender} para {Receiver}: {MessageId}",
                    senderId, receiverId, message.Id);

                // Envia para grupo do remetente
                await _hubContext
                    .Clients
                    .Group(senderId)
                    .SendAsync(ReceiveMessageMethod, message, cancellationToken);

                // Envia para grupo do destinatário
                await _hubContext
                    .Clients
                    .Group(receiverId)
                    .SendAsync(ReceiveMessageMethod, message, cancellationToken);

                _logger.LogInformation(
                    "Mensagem {MessageId} entregue com sucesso.",
                    message.Id);
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning(
                    "Envio de mensagem {MessageId} foi cancelado.",
                    message.Id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Erro ao enviar mensagem {MessageId} de {Sender} para {Receiver}.",
                    message.Id, senderId, receiverId);
                // Pode envelopar em uma NotificationException se desejar
                throw;
            }
        }
    }
}
