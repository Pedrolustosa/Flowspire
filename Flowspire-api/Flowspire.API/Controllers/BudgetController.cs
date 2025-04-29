using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Flowspire.Application.DTOs;
using Flowspire.Application.Interfaces;
using Flowspire.API.Common;
using Flowspire.Application.Common;
using System.Security.Claims;

namespace Flowspire.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BudgetController(IBudgetService budgetService, ILogger<BudgetController> logger) : ControllerBase
{
    private readonly IBudgetService _budgetService = budgetService;
    private readonly ILogger<BudgetController> _logger = logger;

    [HttpPost]
    [Authorize(Roles = "Customer,FinancialAdvisor")]
    public async Task<IActionResult> CreateBudget([FromBody] BudgetDTO budgetDTO)
        => await ControllerHelper.ExecuteAsync(async () =>
        {
            if (!ModelState.IsValid)
                throw new ArgumentException(ErrorMessages.InvalidModelState);

            await _budgetService.CreateAsync(budgetDTO);
        }, _logger, this, SuccessMessages.BudgetCreated);

    [HttpGet("{id}")]
    [Authorize(Roles = "Customer,FinancialAdvisor,Administrator")]
    public async Task<IActionResult> GetBudgetById(int id)
        => await ControllerHelper.ExecuteAsync(async () =>
        {
            var budget = await _budgetService.GetByIdAsync(id);
            if (budget == null)
                throw new KeyNotFoundException(ErrorMessages.BudgetNotFound);

            return budget;
        }, _logger, this, SuccessMessages.BudgetRetrieved);

    [HttpGet("user")]
    [Authorize(Roles = "Customer,FinancialAdvisor,Administrator")]
    public async Task<IActionResult> GetBudgetsByUser()
        => await ControllerHelper.ExecuteAsync(async () =>
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException(ErrorMessages.UserNotFound);

            var budgets = await _budgetService.GetByUserIdAsync(userId);
            return budgets;
        }, _logger, this, SuccessMessages.BudgetsRetrievedByUser);

    [HttpPut("{id}")]
    [Authorize(Roles = "Customer,FinancialAdvisor")]
    public async Task<IActionResult> UpdateBudget(int id, [FromBody] BudgetDTO budgetDTO)
        => await ControllerHelper.ExecuteAsync(async () =>
        {
            if (id != budgetDTO.Id)
                throw new ArgumentException(ErrorMessages.BudgetIdMismatch);

            if (!ModelState.IsValid)
                throw new ArgumentException(ErrorMessages.InvalidModelState);

            await _budgetService.UpdateAsync(budgetDTO);
        }, _logger, this, SuccessMessages.BudgetUpdated);

    [HttpDelete("{id}")]
    [Authorize(Roles = "Customer,FinancialAdvisor")]
    public async Task<IActionResult> DeleteBudget(int id)
        => await ControllerHelper.ExecuteAsync(async () =>
        {
            await _budgetService.DeleteAsync(id);
        }, _logger, this, SuccessMessages.BudgetDeleted);
}
