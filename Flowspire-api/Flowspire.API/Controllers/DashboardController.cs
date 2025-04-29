using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Flowspire.Application.Interfaces;
using Flowspire.Application.DTOs;
using Flowspire.API.Common;
using Flowspire.Application.Common;
using System.Security.Claims;

namespace Flowspire.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DashboardController(IDashboardService dashboardService, ILogger<DashboardController> logger) : ControllerBase
{
    private readonly IDashboardService _dashboardService = dashboardService;
    private readonly ILogger<DashboardController> _logger = logger;

    [HttpGet]
    [Authorize(Roles = "Customer,FinancialAdvisor,Administrator")]
    public async Task<IActionResult> GetDashboard([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        => await ControllerHelper.ExecuteAsync(async () =>
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? throw new Exception(ErrorMessages.UserNotFound);

            var (start, end) = GetDateRange(startDate, endDate);

            var dashboard = await _dashboardService.GetDashboardAsync(userId, start, end);
            return dashboard;
        }, _logger, this, SuccessMessages.DashboardRetrieved);

    [HttpGet("category-summary")]
    [Authorize(Roles = "Customer,FinancialAdvisor,Administrator")]
    public async Task<IActionResult> GetCategorySummary([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate, [FromQuery] string type = "Expense")
        => await ControllerHelper.ExecuteAsync(async () =>
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? throw new Exception(ErrorMessages.UserNotFound);

            if (type != "Expense" && type != "Revenue")
                throw new Exception(ErrorMessages.InvalidTransactionType);

            var (start, end) = GetDateRange(startDate, endDate);

            var summary = await _dashboardService.GetCategorySummaryAsync(userId, start, end, type);
            return summary;
        }, _logger, this, SuccessMessages.CategorySummaryRetrieved);

    [HttpGet("recent-transactions")]
    [Authorize(Roles = "Customer,FinancialAdvisor,Administrator")]
    public async Task<IActionResult> GetRecentTransactions([FromQuery] int limit = 5)
        => await ControllerHelper.ExecuteAsync(async () =>
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? throw new Exception(ErrorMessages.UserNotFound);

            if (limit <= 0 || limit > 50)
                throw new Exception(ErrorMessages.InvalidTransactionLimit);

            var transactions = await _dashboardService.GetRecentTransactionsAsync(userId, limit);
            return transactions;
        }, _logger, this, SuccessMessages.RecentTransactionsRetrieved);

    [HttpGet("current-balance")]
    [Authorize(Roles = "Customer,FinancialAdvisor,Administrator")]
    public async Task<IActionResult> GetCurrentBalance()
        => await ControllerHelper.ExecuteAsync(async () =>
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? throw new Exception(ErrorMessages.UserNotFound);

            var balance = await _dashboardService.GetCurrentBalanceAsync(userId);
            return balance;
        }, _logger, this, SuccessMessages.CurrentBalanceRetrieved);

    [HttpGet("financial-goals")]
    [Authorize(Roles = "Customer,FinancialAdvisor,Administrator")]
    public async Task<IActionResult> GetFinancialGoals()
        => await ControllerHelper.ExecuteAsync(async () =>
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? throw new Exception(ErrorMessages.UserNotFound);

            var goals = await _dashboardService.GetFinancialGoalsAsync(userId);
            return goals;
        }, _logger, this, SuccessMessages.FinancialGoalsRetrieved);

    private (DateTime start, DateTime end) GetDateRange(DateTime? startDate, DateTime? endDate)
    {
        if (startDate.HasValue && endDate.HasValue)
            return (startDate.Value, endDate.Value);

        var now = DateTime.UtcNow;
        var start = new DateTime(now.Year, now.Month, 1);
        var end = start.AddMonths(1).AddDays(-1);
        return (start, end);
    }
}
