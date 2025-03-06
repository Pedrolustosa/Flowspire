using Flowspire.Application.DTOs;

namespace Flowspire.Application.Interfaces;
public interface IDashboardService
{
    Task<DashboardDTO> GetDashboardAsync(string userId, DateTime startDate, DateTime endDate);
}