using Flowspire.Application.DTOs;
using Flowspire.Application.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Flowspire.Domain.Hubs;
using System.Threading.Tasks;

namespace Flowspire.Infra.Services;

public class NotificationService : INotificationService
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationService(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
    }

    public async Task SendMessageAsync(string senderId, string receiverId, MessageDTO message)
    {
        await _hubContext.Clients.Group(senderId).SendAsync("ReceiveMessage", message);
        await _hubContext.Clients.Group(receiverId).SendAsync("ReceiveMessage", message);
    }
}