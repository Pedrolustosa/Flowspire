using Flowspire.Application.DTOs;

namespace Flowspire.Application.Interfaces;
public interface IDashboardService
{
    Task<DashboardDTO> GetDashboardAsync(
        string userId, DateTime startDate, DateTime endDate,
        CancellationToken cancellationToken = default);

    Task<List<CategorySummaryDTO>> GetCategorySummaryAsync(
        string userId, DateTime startDate, DateTime endDate, string type,
        CancellationToken cancellationToken = default);

    Task<List<RecentTransactionDTO>> GetRecentTransactionsAsync(
        string userId, int limit,
        CancellationToken cancellationToken = default);

    Task<BalanceDTO> GetCurrentBalanceAsync(
        string userId,
        CancellationToken cancellationToken = default);

    Task<List<FinancialGoalDTO>> GetFinancialGoalsAsync(
        string userId,
        CancellationToken cancellationToken = default);
}