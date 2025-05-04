using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Flowspire.Domain.Hubs;

[Authorize]
public class NotificationHub(ILogger<NotificationHub> logger) : Hub
{
    private const string UserConnectedMethod = "UserConnected";
    private const string UserDisconnectedMethod = "UserDisconnected";

    private readonly ILogger<NotificationHub> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    private string GetUserId() => Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);

    public override async Task OnConnectedAsync()
    {
        var userId = GetUserId();
        if (string.IsNullOrEmpty(userId))
        {
            _logger.LogWarning("Unauthenticated connection attempt. ConnectionId={ConnectionId}", Context.ConnectionId);
            Context.Abort();
            return;
        }

        using (_logger.BeginScope("NotificationHub OnConnected: {UserId}", userId))
        {
            try
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, userId);
                await Clients.OthersInGroup(userId).SendAsync(UserConnectedMethod, userId);

                _logger.LogInformation("User {UserId} connected. ConnectionId={ConnectionId}", userId, Context.ConnectionId);
                await base.OnConnectedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in OnConnectedAsync for user {UserId}", userId);
                throw;
            }
        }
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var userId = GetUserId();

        using (_logger.BeginScope("NotificationHub OnDisconnected: {UserId}", userId))
        {
            try
            {
                await base.OnDisconnectedAsync(exception);

                if (!string.IsNullOrEmpty(userId))
                {
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
                    await Clients.OthersInGroup(userId).SendAsync(UserDisconnectedMethod, userId);

                    _logger.LogInformation("User {UserId} disconnected. ConnectionId={ConnectionId}", userId, Context.ConnectionId);
                }

                if (exception != null)
                {
                    _logger.LogError(exception, "Disconnect error for user {UserId}", userId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in OnDisconnectedAsync for user {UserId}", userId);
            }
        }
    }
}