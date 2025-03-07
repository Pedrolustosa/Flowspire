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
}