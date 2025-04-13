using Flowspire.Application.Interfaces;
using Flowspire.Application.DTOs;
using System.Globalization;
using Flowspire.Domain.Interfaces;
using Flowspire.Domain.Enums;

namespace Flowspire.Application.Services;

public class DashboardService(
    IFinancialTransactionRepository transactionRepository,
    IBudgetRepository budgetRepository
) : IDashboardService
{
    private readonly IFinancialTransactionRepository _transactionRepository = transactionRepository;
    private readonly IBudgetRepository _budgetRepository = budgetRepository;

    public async Task<DashboardDTO> GetDashboardAsync(string userId, DateTime startDate, DateTime endDate)
    {
        try
        {
            var transactions = await _transactionRepository.GetByUserIdAsync(userId);
            var filteredTransactions = transactions
                .Where(t => t.Date >= startDate && t.Date <= endDate)
                .ToList();

            var totalIncome = filteredTransactions
                .Where(t => t.Type == TransactionType.Income)
                .Sum(t => t.Amount);
            var totalExpenses = filteredTransactions
                .Where(t => t.Type == TransactionType.Expense)
                .Sum(t => t.Amount);

            var budgets = await _budgetRepository.GetByUserIdAsync(userId);
            var activeBudgets = budgets
                .Where(b => b.StartDate <= endDate && b.EndDate >= startDate)
                .ToList();

            var budgetStatuses = new List<BudgetStatusDTO>();
            var alerts = new List<string>();

            foreach (var budget in activeBudgets)
            {
                var spentAmount = filteredTransactions
                    .Where(t => t.CategoryId == budget.CategoryId
                                && t.Date >= budget.StartDate
                                && t.Date <= budget.EndDate
                                && t.Type == TransactionType.Expense)
                    .Sum(t => t.Amount);

                var percentageUsed = budget.Amount > 0 ? (double)(spentAmount / budget.Amount) * 100 : 0;

                budgetStatuses.Add(new BudgetStatusDTO
                {
                    CategoryName = budget.Category.Name,
                    BudgetAmount = budget.Amount,
                    SpentAmount = spentAmount,
                    PercentageUsed = (decimal)percentageUsed
                });

                if (percentageUsed >= 90)
                {
                    alerts.Add($"Atenção: O orçamento de {budget.Category.Name} atingiu {percentageUsed:F2}% do limite ({spentAmount}/{budget.Amount}).");
                }
            }

            var monthlyHistory = new List<MonthlySummaryDTO>();
            for (int i = 2; i >= 0; i--)
            {
                var monthStart = startDate.AddMonths(-i).Date;
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

            var previousStart = startDate.AddMonths(-1);
            var previousEnd = startDate.AddDays(-1);
            var previousTransactions = transactions
                .Where(t => t.Date >= previousStart && t.Date <= previousEnd)
                .ToList();

            var categoryTrends = filteredTransactions
                .GroupBy(t => t.Category.Name)
                .Select(g => new CategoryTrendDTO
                {
                    CategoryName = g.Key,
                    CurrentPeriodExpenses = g.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount),
                    PreviousPeriodExpenses = previousTransactions
                        .Where(t => t.Category.Name == g.Key && t.Type == TransactionType.Expense)
                        .Sum(t => t.Amount),
                    TrendPercentage = CalculateTrendPercentage(
                        g.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount),
                        previousTransactions.Where(t => t.Category.Name == g.Key && t.Type == TransactionType.Expense).Sum(t => t.Amount))
                })
                .ToList();

            var categorySummary = filteredTransactions
                .GroupBy(t => t.Category.Name)
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
        }
        catch (Exception ex)
        {
            throw new Exception("Erro inesperado ao gerar o dashboard financeiro.", ex);
        }
    }

    public async Task<List<CategorySummaryDTO>> GetCategorySummaryAsync(string userId, DateTime startDate, DateTime endDate, string type)
    {
        try
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentException("User ID is required.", nameof(userId));
            if (string.IsNullOrEmpty(type) || (type != "Expense" && type != "Revenue"))
                throw new ArgumentException("Invalid type. Use 'Expense' or 'Revenue'.", nameof(type));

            var transactions = await _transactionRepository.GetByUserIdAsync(userId);
            var filteredTransactions = transactions
                .Where(t => t.Date >= startDate && t.Date <= endDate)
                .ToList();

            var summary = filteredTransactions
                .Where(t => (type == "Expense" && t.Type == TransactionType.Expense) ||
                            (type == "Revenue" && t.Type == TransactionType.Income))
                .GroupBy(t => t.Category.Name)
                .Select(g => new CategorySummaryDTO
                {
                    CategoryName = g.Key,
                    Income = type == "Revenue" ? g.Sum(t => t.Amount) : 0,
                    Expenses = type == "Expense" ? g.Sum(t => t.Amount) : 0
                })
                .ToList();

            return summary;
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao obter o resumo por categoria ({type}).", ex);
        }
    }

    public async Task<List<RecentTransactionDTO>> GetRecentTransactionsAsync(string userId, int limit)
    {
        try
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentException("User ID is required.", nameof(userId));
            if (limit <= 0)
                throw new ArgumentException("Limit must be positive.", nameof(limit));

            var transactions = await _transactionRepository.GetByUserIdAsync(userId);
            var recentTransactions = transactions
                .OrderByDescending(t => t.Date)
                .Take(limit)
                .Select(t => new RecentTransactionDTO
                {
                    Description = t.Description,
                    Amount = t.Amount,
                    Type = t.Type == TransactionType.Income
                            ? "Revenue"
                            : t.Type == TransactionType.Expense
                                ? "Expense"
                                : t.Type.ToString(),
                    Category = t.Category != null ? t.Category.Name : "Uncategorized",
                    Date = t.Date
                })
                .ToList();

            return recentTransactions;
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao obter transações recentes.", ex);
        }
    }

    public async Task<BalanceDTO> GetCurrentBalanceAsync(string userId)
    {
        try
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentException("User ID is required.", nameof(userId));

            var transactions = await _transactionRepository.GetByUserIdAsync(userId);
            var totalRevenue = transactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount);
            var totalExpense = transactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount);

            return new BalanceDTO
            {
                TotalRevenue = totalRevenue,
                TotalExpense = totalExpense,
                CurrentBalance = totalRevenue - totalExpense
            };
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao obter o saldo atual.", ex);
        }
    }

    public async Task<List<FinancialGoalDTO>> GetFinancialGoalsAsync(string userId)
    {
        try
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentException("User ID is required.", nameof(userId));

            var budgets = await _budgetRepository.GetByUserIdAsync(userId);
            var transactions = await _transactionRepository.GetByUserIdAsync(userId);

            var financialGoals = budgets
                .Select(b => {
                    var currentSpent = transactions
                        .Where(t => t.CategoryId == b.CategoryId
                                    && t.Date >= b.StartDate
                                    && t.Date <= b.EndDate
                                    && t.Type == TransactionType.Expense)
                        .Sum(t => t.Amount);
                    return new FinancialGoalDTO
                    {
                        Name = b.Category.Name,
                        TargetAmount = b.Amount,
                        CurrentAmount = currentSpent,
                        Deadline = b.EndDate,
                        ProgressPercentage = b.Amount > 0 ? (double)(currentSpent / b.Amount) * 100 : 0
                    };
                })
                .Where(g => g.CurrentAmount > 0)
                .ToList();

            return financialGoals;
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao obter metas financeiras.", ex);
        }
    }

    private decimal CalculateTrendPercentage(decimal current, decimal previous)
    {
        if (previous == 0) return current > 0 ? 100 : 0;
        return ((current - previous) / previous) * 100;
    }
}
