using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Flowspire.Application.Interfaces;
using Flowspire.Application.DTOs;
using System.Security.Claims;

namespace Flowspire.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BudgetController(IBudgetService budgetService) : ControllerBase
{
    private readonly IBudgetService _budgetService = budgetService;

    [HttpPost]
    [Authorize(Roles = "Customer, FinancialAdvisor")]
    public async Task<IActionResult> AddBudget([FromBody] BudgetDTO budgetDto)
    {
        try
        {
            budgetDto.UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _budgetService.AddBudgetAsync(budgetDto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpGet("user")]
    [Authorize(Roles = "Customer, FinancialAdvisor, Administrator")]
    public async Task<IActionResult> GetBudgets()
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var budgets = await _budgetService.GetBudgetsByUserIdAsync(userId);
            return Ok(budgets);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}