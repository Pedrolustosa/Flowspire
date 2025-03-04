using Microsoft.AspNetCore.SignalR;
using Flowspire.Application.Interfaces;
using Flowspire.Domain.Hubs;

namespace Flowspire.Infra.Services;
public class NotificationService : INotificationService
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationService(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
    }

    public async Task SendNotificationAsync(string userId, string message)
    {
        await _hubContext.Clients.User(userId).SendAsync("ReceiveNotification", message);
    }
}