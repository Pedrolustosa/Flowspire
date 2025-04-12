using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Flowspire.Application.Interfaces;
using Flowspire.Application.DTOs;
using System.Security.Claims;

namespace Flowspire.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class FinancialTransactionController(IFinancialTransactionService transactionService) : ControllerBase
{
    private readonly IFinancialTransactionService _transactionService = transactionService;

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var transaction = await _transactionService.GetByIdAsync(id);
        if (transaction == null)
            return NotFound("Transaction not found.");

        return Ok(transaction);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var transactions = await _transactionService.GetAllAsync();
        return Ok(transactions);
    }

    [HttpGet("user")]
    public async Task<IActionResult> GetByUserId()
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
            return Unauthorized("User not found in token.");

        var transactions = await _transactionService.GetByUserIdAsync(userId);
        return Ok(transactions);
    }

    [HttpGet("range")]
    public async Task<IActionResult> GetByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var transactions = await _transactionService.GetByDateRangeAsync(startDate, endDate);
        return Ok(transactions);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] FinancialTransactionDTO transactionDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
            return Unauthorized("User not found in token.");

        transactionDTO.UserId = userId;

        await _transactionService.CreateAsync(transactionDTO);
        return CreatedAtAction(nameof(GetById), new { id = transactionDTO.Id }, transactionDTO);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] FinancialTransactionDTO transactionDTO)
    {
        if (id != transactionDTO.Id)
            return BadRequest("The provided IDs do not match.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await _transactionService.UpdateAsync(transactionDTO);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _transactionService.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }

        return NoContent();
    }
}
