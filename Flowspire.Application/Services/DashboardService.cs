using Flowspire.Application.Interfaces;
using Flowspire.Application.DTOs;
using System.Globalization;
using Flowspire.Domain.Interfaces;

namespace Flowspire.Application.Services;

public class DashboardService(ITransactionRepository transactionRepository, 
                              IBudgetRepository budgetRepository) : IDashboardService
{
    private readonly ITransactionRepository _transactionRepository = transactionRepository;
    private readonly IBudgetRepository _budgetRepository = budgetRepository;

    public async Task<DashboardDTO> GetDashboardAsync(string userId, DateTime startDate, DateTime endDate)
    {
        try
        {
            var transactions = await _transactionRepository.GetByUserIdAsync(userId);
            var filteredTransactions = transactions
                .Where(t => t.Date >= startDate && t.Date <= endDate)
                .ToList();

            var totalIncome = filteredTransactions.Where(t => t.Amount > 0).Sum(t => t.Amount);
            var totalExpenses = filteredTransactions.Where(t => t.Amount < 0).Sum(t => Math.Abs(t.Amount));

            var budgets = await _budgetRepository.GetByUserIdAsync(userId);
            var activeBudgets = budgets
                .Where(b => b.StartDate <= endDate && b.EndDate >= startDate)
                .ToList();

            var budgetStatuses = new List<BudgetStatusDTO>();
            var alerts = new List<string>();
            foreach (var budget in activeBudgets)
            {
                var spentAmount = filteredTransactions
                    .Where(t => t.CategoryId == budget.CategoryId && t.Date >= budget.StartDate && t.Date <= budget.EndDate)
                    .Sum(t => Math.Abs(t.Amount));

                var percentageUsed = budget.Amount > 0 ? (spentAmount / budget.Amount) * 100 : 0;

                budgetStatuses.Add(new BudgetStatusDTO
                {
                    BudgetId = budget.Id,
                    CategoryName = budget.Category.Name,
                    BudgetAmount = budget.Amount,
                    SpentAmount = spentAmount,
                    PercentageUsed = percentageUsed
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
                    Income = monthTransactions.Where(t => t.Amount > 0).Sum(t => t.Amount),
                    Expenses = monthTransactions.Where(t => t.Amount < 0).Sum(t => Math.Abs(t.Amount))
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
                    CurrentPeriodExpenses = g.Where(t => t.Amount < 0).Sum(t => Math.Abs(t.Amount)),
                    PreviousPeriodExpenses = previousTransactions
                        .Where(t => t.Category.Name == g.Key && t.Amount < 0)
                        .Sum(t => Math.Abs(t.Amount)),
                    TrendPercentage = CalculateTrendPercentage(
                        g.Where(t => t.Amount < 0).Sum(t => Math.Abs(t.Amount)),
                        previousTransactions.Where(t => t.Category.Name == g.Key && t.Amount < 0).Sum(t => Math.Abs(t.Amount)))
                })
                .ToList();

            var categorySummary = filteredTransactions
                .GroupBy(t => t.Category.Name)
                .Select(g => new CategorySummaryDTO
                {
                    CategoryName = g.Key,
                    Income = g.Where(t => t.Amount > 0).Sum(t => t.Amount),
                    Expenses = g.Where(t => t.Amount < 0).Sum(t => Math.Abs(t.Amount))
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

    private decimal CalculateTrendPercentage(decimal current, decimal previous)
    {
        if (previous == 0) return current > 0 ? 100 : 0;
        return ((current - previous) / previous) * 100;
    }
}