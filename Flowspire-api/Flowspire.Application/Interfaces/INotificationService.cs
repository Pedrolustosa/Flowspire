namespace Flowspire.Application.Interfaces;
public interface INotificationService
{
    Task SendNotificationAsync(string userId, string message);
}