using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Flowspire.Domain.Hubs;

[Authorize]
public class NotificationHub : Hub
{
    private readonly ILogger<NotificationHub> _logger;

    public NotificationHub(ILogger<NotificationHub> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public override async Task OnConnectedAsync()
    {
        var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!string.IsNullOrWhiteSpace(userId))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);
            await Clients.All.SendAsync("UserConnected", userId);
            _logger.LogInformation("User {UserId} connected to NotificationHub. ConnectionId: {ConnectionId}", userId, Context.ConnectionId);
        }
        else
        {
            _logger.LogWarning("Unauthenticated user attempted to connect to NotificationHub. ConnectionId: {ConnectionId}", Context.ConnectionId);
        }
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!string.IsNullOrWhiteSpace(userId))
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
            await Clients.All.SendAsync("UserDisconnected", userId);
            _logger.LogInformation("User {UserId} disconnected from NotificationHub. ConnectionId: {ConnectionId}", userId, Context.ConnectionId);
            if (exception != null)
            {
                _logger.LogError(exception, "Error during disconnection of user {UserId}.", userId);
            }
        }
        await base.OnDisconnectedAsync(exception);
    }
}