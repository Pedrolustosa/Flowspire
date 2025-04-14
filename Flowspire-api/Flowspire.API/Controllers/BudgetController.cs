using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Flowspire.Application.DTOs;
using Flowspire.Application.Interfaces;
using System.Security.Claims;

namespace Flowspire.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BudgetController(IBudgetService budgetService) : ControllerBase
{
    private readonly IBudgetService _budgetService = budgetService;

    [HttpPost]
    [Authorize(Roles = "Customer,FinancialAdvisor")]
    public async Task<IActionResult> CreateBudget([FromBody] BudgetDTO budgetDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _budgetService.CreateAsync(budgetDTO);
        return CreatedAtAction(nameof(GetBudgetById), new { id = budgetDTO.Id }, budgetDTO);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Customer,FinancialAdvisor,Administrator")]
    public async Task<IActionResult> GetBudgetById(int id)
    {
        var budget = await _budgetService.GetByIdAsync(id);
        if (budget == null)
            return NotFound("Budget not found.");
        return Ok(budget);
    }

    [HttpGet("user")]
    [Authorize(Roles = "Customer,FinancialAdvisor,Administrator")]
    public async Task<IActionResult> GetBudgetsByUser()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized("User not identified.");

        var budgets = await _budgetService.GetByUserIdAsync(userId);
        return Ok(budgets);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Customer,FinancialAdvisor")]
    public async Task<IActionResult> UpdateBudget(int id, [FromBody] BudgetDTO budgetDTO)
    {
        if (id != budgetDTO.Id)
            return BadRequest("ID mismatch.");
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _budgetService.UpdateAsync(budgetDTO);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Customer,FinancialAdvisor")]
    public async Task<IActionResult> DeleteBudget(int id)
    {
        await _budgetService.DeleteAsync(id);
        return NoContent();
    }
}