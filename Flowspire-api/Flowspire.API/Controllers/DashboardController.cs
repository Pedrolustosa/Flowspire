using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Flowspire.Application.Interfaces;
using Flowspire.Application.DTOs;
using System.Security.Claims;

namespace Flowspire.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DashboardController(IDashboardService dashboardService) : ControllerBase
{
    private readonly IDashboardService _dashboardService = dashboardService;

    [HttpGet]
    [Authorize(Roles = "Customer, FinancialAdvisor, Administrator")]
    public async Task<IActionResult> GetDashboard([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            if (startDate == default || endDate == default)
            {
                var now = DateTime.UtcNow;
                startDate = new DateTime(now.Year, now.Month, 1);
                endDate = startDate.AddMonths(1).AddDays(-1);
            }

            var dashboard = await _dashboardService.GetDashboardAsync(userId, startDate, endDate);
            return Ok(dashboard);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpGet("category-summary")]
    [Authorize(Roles = "Customer, FinancialAdvisor, Administrator")]
    public async Task<IActionResult> GetCategorySummary([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] string type = "Expense")
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            if (startDate == default || endDate == default)
            {
                var now = DateTime.UtcNow;
                startDate = new DateTime(now.Year, now.Month, 1);
                endDate = startDate.AddMonths(1).AddDays(-1);
            }

            if (type != "Expense" && type != "Revenue")
                return BadRequest(new { Error = "Type must be 'Expense' or 'Revenue'." });

            var summary = await _dashboardService.GetCategorySummaryAsync(userId, startDate, endDate, type);
            return Ok(summary);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpGet("recent-transactions")]
    [Authorize(Roles = "Customer, FinancialAdvisor, Administrator")]
    public async Task<IActionResult> GetRecentTransactions([FromQuery] int limit = 5)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            if (limit <= 0 || limit > 50)
                return BadRequest(new { Error = "Limit must be between 1 and 50." });

            var transactions = await _dashboardService.GetRecentTransactionsAsync(userId, limit);
            return Ok(transactions);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpGet("current-balance")]
    [Authorize(Roles = "Customer, FinancialAdvisor, Administrator")]
    public async Task<IActionResult> GetCurrentBalance()
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var balance = await _dashboardService.GetCurrentBalanceAsync(userId);
            return Ok(balance);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpGet("financial-goals")]
    [Authorize(Roles = "Customer, FinancialAdvisor, Administrator")]
    public async Task<IActionResult> GetFinancialGoals()
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var goals = await _dashboardService.GetFinancialGoalsAsync(userId);
            return Ok(goals);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}