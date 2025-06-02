using Flowspire.Application.DTOs;
using Flowspire.Application.Interfaces;
using Flowspire.Application.Common;
using Flowspire.Domain.Enums;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace Flowspire.Application.Services;

public class DashboardService : IDashboardService
{
    private readonly IFinancialTransactionService _transactionService;
    private readonly ILogger<DashboardService> _logger;

    public DashboardService(
        IFinancialTransactionService transactionService,
        ILogger<DashboardService> logger)
    {
        _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<DashboardDTO> GetDashboardAsync(
        string userId, DateTime startDate, DateTime endDate,
        CancellationToken cancellationToken = default)
        => await ServiceHelper.ExecuteAsync(async () =>
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("User ID must be provided.", nameof(userId));
            if (startDate > endDate)
                throw new ArgumentException("Start date must be before end date.", nameof(startDate));

            _logger.LogInformation("Building dashboard for user {UserId} from {StartDate} to {EndDate}...", userId, startDate, endDate);

            var transactions = await _transactionService.GetByUserIdAsync(userId);

            var filtered = transactions.Where(t => t.Date >= startDate && t.Date <= endDate).ToList();

            var totalIncome = filtered.Where(t => t.TransactionType == nameof(TransactionType.Income)).Sum(t => t.Amount);
            var totalExpenses = filtered.Where(t => t.TransactionType == nameof(TransactionType.Expense)).Sum(t => t.Amount);

            var history = BuildMonthlyHistory(transactions, startDate);
            var trends = BuildCategoryTrends(transactions, startDate);
            var summary = BuildCategorySummary(filtered);

            _logger.LogInformation("Dashboard built for user {UserId}.", userId);

            return new DashboardDTO
            {
                TotalIncome = totalIncome,
                TotalExpenses = totalExpenses,
                MonthlyHistory = history,
                CategoryTrends = trends,
                CategorySummary = summary
            };
        }, _logger, nameof(GetDashboardAsync));

    #region Helpers

    private List<MonthlySummaryDTO> BuildMonthlyHistory(
    IEnumerable<FinancialTransactionDTO> txs,
    DateTime startDate)
    {
        var list = new List<MonthlySummaryDTO>();
        for (int i = 2; i >= 0; i--)
        {
            var mStart = startDate.AddMonths(-i);
            var mEnd = mStart.AddMonths(1).AddDays(-1);
            var mTx = txs.Where(t => t.Date >= mStart && t.Date <= mEnd);

            list.Add(new MonthlySummaryDTO
            {
                Month    = mStart.ToString("MMMM yyyy", CultureInfo.CurrentCulture),
                Income   = mTx.Where(t => t.TransactionType == nameof(TransactionType.Income)).Sum(t => t.Amount),
                Expenses = mTx.Where(t => t.TransactionType == nameof(TransactionType.Expense)).Sum(t => t.Amount)
            });
        }
        return list;
    }

    private List<CategoryTrendDTO> BuildCategoryTrends(
        IEnumerable<FinancialTransactionDTO> txs,
        DateTime startDate)
    {
        var currStart = startDate;
        var prevStart = currStart.AddMonths(-1);
        var prevEnd = currStart.AddDays(-1);

        var curr = txs.Where(t => t.Date >= currStart);
        var prev = txs.Where(t => t.Date >= prevStart && t.Date <= prevEnd);

        return curr
            .GroupBy(t => t.CategoryName ?? "Uncategorized")
            .Select(g =>
            {
                var currExp = g
                    .Where(t => t.TransactionType == nameof(TransactionType.Expense))
                    .Sum(t => t.Amount);

                var prevExp = prev
                    .Where(t => t.CategoryName == g.Key && t.TransactionType == nameof(TransactionType.Expense))
                    .Sum(t => t.Amount);

                // tudo em decimal também aqui
                var trend = prevExp == 0m
                    ? (currExp > 0m ? 100m : 0m)
                    : (currExp - prevExp) / prevExp * 100m;

                return new CategoryTrendDTO
                {
                    CategoryName           = g.Key,
                    CurrentPeriodExpenses  = currExp,
                    PreviousPeriodExpenses = prevExp,
                    TrendPercentage        = trend
                };
            })
            .ToList();
    }

    private List<CategorySummaryDTO> BuildCategorySummary(
        IEnumerable<FinancialTransactionDTO> txs)
    {
        return txs
            .GroupBy(t => t.CategoryName ?? "Uncategorized")
            .Select(g => new CategorySummaryDTO
            {
                CategoryName = g.Key,
                Income       = g
                    .Where(t => t.TransactionType == nameof(TransactionType.Income))
                    .Sum(t => t.Amount),
                Expenses     = g
                    .Where(t => t.TransactionType == nameof(TransactionType.Expense))
                    .Sum(t => t.Amount)
            })
            .ToList();
    }

    public Task<List<CategorySummaryDTO>> GetCategorySummaryAsync(string userId, DateTime startDate, DateTime endDate, string type, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<RecentTransactionDTO>> GetRecentTransactionsAsync(string userId, int limit, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<BalanceDTO> GetCurrentBalanceAsync(string userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<FinancialGoalDTO>> GetFinancialGoalsAsync(string userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    #endregion
}
