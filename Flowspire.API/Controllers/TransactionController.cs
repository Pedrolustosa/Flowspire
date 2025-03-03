// ... (usings existentes)
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
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpPost]
    [Authorize(Roles = "Customer, FinancialAdvisor")]
    public async Task<IActionResult> AddTransaction([FromBody] TransactionDTO transactionDto)
    {
        transactionDto.UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var result = await _transactionService.AddTransactionAsync(transactionDto);
        return Ok(result);
    }

    [HttpGet("user/{userId}")]
    [Authorize(Roles = "Administrator, FinancialAdvisor")]
    public async Task<IActionResult> GetTransactions(string userId)
    {
        var authenticatedUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (authenticatedUserId != userId)
            return Forbid();

        var transactions = await _transactionService.GetTransactionsByUserIdAsync(userId);
        return Ok(transactions);
    }
}