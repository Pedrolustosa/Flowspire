using Flowspire.Application.DTOs;

namespace Flowspire.Application.Interfaces;
public interface IDashboardService
{
    Task<DashboardDTO> GetDashboardAsync(string userId, DateTime startDate, DateTime endDate);
    Task<List<CategorySummaryDTO>> GetCategorySummaryAsync(string userId, DateTime startDate, DateTime endDate, string type);
    Task<List<RecentTransactionDTO>> GetRecentTransactionsAsync(string userId, int limit);
    Task<BalanceDTO> GetCurrentBalanceAsync(string userId);
    Task<List<FinancialGoalDTO>> GetFinancialGoalsAsync(string userId);
}