using Microsoft.AspNetCore.SignalR;
using Flowspire.Application.Interfaces;
using Flowspire.Domain.Hubs;

namespace Flowspire.Infra.Services;

public class NotificationService(IHubContext<NotificationHub> hubContext) : INotificationService
{
    private readonly IHubContext<NotificationHub> _hubContext = hubContext;

    public async Task SendNotificationAsync(string userId, string message)
    {
        try
        {
            await _hubContext.Clients.User(userId).SendAsync("ReceiveNotification", message);
        }
        catch (HubException ex)
        {
            throw new Exception("Erro ao enviar notificação via SignalR.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro inesperado ao enviar notificação.", ex);
        }
    }
}