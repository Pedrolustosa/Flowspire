using Flowspire.Application.DTOs;
using Flowspire.Application.Interfaces;
using Flowspire.Application.Common;
using Flowspire.Domain.Enums;
using Flowspire.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace Flowspire.Application.Services;

public class DashboardService(
    IFinancialTransactionRepository transactionRepository,
    IBudgetRepository budgetRepository,
    ILogger<DashboardService> logger) : IDashboardService
{
    private readonly IFinancialTransactionRepository _transactionRepository = transactionRepository;
    private readonly IBudgetRepository _budgetRepository = budgetRepository;
    private readonly ILogger<DashboardService> _logger = logger;

    public async Task<DashboardDTO> GetDashboardAsync(string userId, DateTime startDate, DateTime endDate)
        => await ServiceHelper.ExecuteAsync(async () =>
        {
            var transactions = await _transactionRepository.GetByUserIdAsync(userId);
            var budgets = await _budgetRepository.GetByUserIdAsync(userId);

            var filteredTransactions = transactions
                .Where(t => t.Date >= startDate && t.Date <= endDate)
                .ToList();

            var totalIncome = filteredTransactions
                .Where(t => t.Type == TransactionType.Income)
                .Sum(t => t.Amount);

            var totalExpenses = filteredTransactions
                .Where(t => t.Type == TransactionType.Expense)
                .Sum(t => t.Amount);

            var activeBudgets = budgets
                .Where(b => b.StartDate <= endDate && b.EndDate >= startDate)
                .ToList();

            var budgetStatuses = new List<BudgetStatusDTO>();
            var alerts = new List<string>();

            foreach (var budget in activeBudgets)
            {
                var spentAmount = filteredTransactions
                    .Where(t => t.CategoryId == budget.CategoryId && t.Type == TransactionType.Expense)
                    .Sum(t => t.Amount);

                var percentageUsed = budget.Amount > 0 ? (double)(spentAmount / budget.Amount) * 100 : 0;
                var categoryName = budget.Category?.Name ?? "Uncategorized";

                budgetStatuses.Add(new BudgetStatusDTO
                {
                    CategoryName = categoryName,
                    BudgetAmount = budget.Amount,
                    SpentAmount = spentAmount,
                    PercentageUsed = (decimal)percentageUsed
                });

                if (percentageUsed >= 90)
                    alerts.Add($"Warning: Budget {categoryName} reached {percentageUsed:F2}% ({spentAmount}/{budget.Amount}).");
            }

            var monthlyHistory = BuildMonthlyHistory(transactions, startDate);

            var categoryTrends = BuildCategoryTrends(transactions, startDate);

            var categorySummary = filteredTransactions
                .GroupBy(t => t.Category?.Name ?? "Uncategorized")
                .Select(g => new CategorySummaryDTO
                {
                    CategoryName = g.Key,
                    Income = g.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount),
                    Expenses = g.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount)
                })
                .ToList();

            return new DashboardDTO
            {
                TotalIncome = totalIncome,
                TotalExpenses = totalExpenses,
                Budgets = budgetStatuses,
                Alerts = alerts,
                MonthlyHistory = monthlyHistory,
                CategoryTrends = categoryTrends,
                CategorySummary = categorySummary
            };
        }, _logger, nameof(GetDashboardAsync));

    public async Task<List<CategorySummaryDTO>> GetCategorySummaryAsync(string userId, DateTime startDate, DateTime endDate, string type)
        => await ServiceHelper.ExecuteAsync(async () =>
        {
            if (type != "Expense" && type != "Revenue")
                throw new ArgumentException("Invalid type. Use 'Expense' or 'Revenue'.");

            var transactions = await _transactionRepository.GetByUserIdAsync(userId);

            var filteredTransactions = transactions
                .Where(t => t.Date >= startDate && t.Date <= endDate)
                .ToList();

            return filteredTransactions
                .Where(t => (type == "Expense" && t.Type == TransactionType.Expense) ||
                            (type == "Revenue" && t.Type == TransactionType.Income))
                .GroupBy(t => t.Category?.Name ?? "Uncategorized")
                .Select(g => new CategorySummaryDTO
                {
                    CategoryName = g.Key,
                    Income = type == "Revenue" ? g.Sum(t => t.Amount) : 0,
                    Expenses = type == "Expense" ? g.Sum(t => t.Amount) : 0
                })
                .ToList();
        }, _logger, nameof(GetCategorySummaryAsync));

    public async Task<List<RecentTransactionDTO>> GetRecentTransactionsAsync(string userId, int limit)
        => await ServiceHelper.ExecuteAsync(async () =>
        {
            if (limit <= 0)
                throw new ArgumentException("Limit must be positive.");

            var transactions = await _transactionRepository.GetByUserIdAsync(userId);

            return transactions
                .OrderByDescending(t => t.Date)
                .Take(limit)
                .Select(t => new RecentTransactionDTO
                {
                    Description = t.Description,
                    Amount = t.Amount,
                    Type = t.Type == TransactionType.Income ? "Revenue" : "Expense",
                    Category = t.Category?.Name ?? "Uncategorized",
                    Date = t.Date
                })
                .ToList();
        }, _logger, nameof(GetRecentTransactionsAsync));

    public async Task<BalanceDTO> GetCurrentBalanceAsync(string userId)
        => await ServiceHelper.ExecuteAsync(async () =>
        {
            var transactions = await _transactionRepository.GetByUserIdAsync(userId);

            var totalRevenue = transactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount);
            var totalExpense = transactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount);

            return new BalanceDTO
            {
                TotalRevenue = totalRevenue,
                TotalExpense = totalExpense,
                CurrentBalance = totalRevenue - totalExpense
            };
        }, _logger, nameof(GetCurrentBalanceAsync));

    public async Task<List<FinancialGoalDTO>> GetFinancialGoalsAsync(string userId)
        => await ServiceHelper.ExecuteAsync(async () =>
        {
            var budgets = await _budgetRepository.GetByUserIdAsync(userId);
            var transactions = await _transactionRepository.GetByUserIdAsync(userId);

            return budgets.Select(b =>
            {
                var spent = transactions
                    .Where(t => t.CategoryId == b.CategoryId && t.Type == TransactionType.Expense)
                    .Sum(t => t.Amount);

                return new FinancialGoalDTO
                {
                    Name = b.Category?.Name ?? "Uncategorized",
                    TargetAmount = b.Amount,
                    CurrentAmount = spent,
                    Deadline = b.EndDate,
                    ProgressPercentage = b.Amount > 0 ? (double)(spent / b.Amount) * 100 : 0
                };
            }).Where(g => g.CurrentAmount > 0).ToList();
        }, _logger, nameof(GetFinancialGoalsAsync));

    private List<MonthlySummaryDTO> BuildMonthlyHistory(IEnumerable<FinancialTransaction> transactions, DateTime startDate)
    {
        var monthlyHistory = new List<MonthlySummaryDTO>();

        for (int i = 2; i >= 0; i--)
        {
            var monthStart = startDate.AddMonths(-i);
            var monthEnd = monthStart.AddMonths(1).AddDays(-1);

            var monthTransactions = transactions
                .Where(t => t.Date >= monthStart && t.Date <= monthEnd)
                .ToList();

            monthlyHistory.Add(new MonthlySummaryDTO
            {
                Month = monthStart.ToString("MMMM yyyy", CultureInfo.CurrentCulture),
                Income = monthTransactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount),
                Expenses = monthTransactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount)
            });
        }

        return monthlyHistory;
    }

    private List<CategoryTrendDTO> BuildCategoryTrends(IEnumerable<FinancialTransaction> transactions, DateTime startDate)
    {
        var currentStart = startDate;
        var previousStart = currentStart.AddMonths(-1);
        var previousEnd = currentStart.AddDays(-1);

        var currentPeriod = transactions
            .Where(t => t.Date >= currentStart)
            .ToList();

        var previousPeriod = transactions
            .Where(t => t.Date >= previousStart && t.Date <= previousEnd)
            .ToList();

        return currentPeriod
            .GroupBy(t => t.Category?.Name ?? "Uncategorized")
            .Select(g =>
            {
                var currentExpense = g.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount);
                var previousExpense = previousPeriod
                    .Where(t => (t.Category?.Name ?? "Uncategorized") == g.Key && t.Type == TransactionType.Expense)
                    .Sum(t => t.Amount);

                return new CategoryTrendDTO
                {
                    CategoryName = g.Key,
                    CurrentPeriodExpenses = currentExpense,
                    PreviousPeriodExpenses = previousExpense,
                    TrendPercentage = CalculateTrendPercentage(currentExpense, previousExpense)
                };
            }).ToList();
    }

    private decimal CalculateTrendPercentage(decimal current, decimal previous)
    {
        if (previous == 0) return current > 0 ? 100 : 0;
        return ((current - previous) / previous) * 100;
    }
}
