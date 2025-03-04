using Flowspire.Application.DTOs;
using Flowspire.Application.Interfaces;
using Flowspire.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        budgetDto.UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var result = await _budgetService.AddBudgetAsync(budgetDto);
        return Ok(result);
    }

    [HttpGet("user")]
    [Authorize(Roles = "Customer, FinancialAdvisor, Administrator")]
    public async Task<IActionResult> GetBudgets()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var budgets = await _budgetService.GetBudgetsByUserIdAsync(userId);
        return Ok(budgets);
    }
}
