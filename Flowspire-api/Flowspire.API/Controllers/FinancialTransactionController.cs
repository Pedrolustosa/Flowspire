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
public class FinancialTransactionController(IFinancialTransactionService transactionService, ILogger<FinancialTransactionController> logger) : ControllerBase
{
    private readonly IFinancialTransactionService _transactionService = transactionService;
    private readonly ILogger<FinancialTransactionController> _logger = logger;

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
        => await ControllerHelper.ExecuteAsync(async () =>
        {
            var transaction = await _transactionService.GetByIdAsync(id);
            if (transaction == null)
                throw new Exception(ErrorMessages.TransactionNotFound);

            return transaction;
        }, _logger, this, SuccessMessages.TransactionRetrieved);

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => await ControllerHelper.ExecuteAsync(async () =>
        {
            var transactions = await _transactionService.GetAllAsync();
            return transactions;
        }, _logger, this, SuccessMessages.TransactionsRetrieved);

    [HttpGet("user")]
    public async Task<IActionResult> GetByUserId()
        => await ControllerHelper.ExecuteAsync(async () =>
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                throw new Exception(ErrorMessages.UserNotFound);

            var transactions = await _transactionService.GetByUserIdAsync(userId);
            return transactions;
        }, _logger, this, SuccessMessages.TransactionsRetrievedByUser);

    [HttpGet("range")]
    public async Task<IActionResult> GetByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        => await ControllerHelper.ExecuteAsync(async () =>
        {
            var transactions = await _transactionService.GetByDateRangeAsync(startDate, endDate);
            return transactions;
        }, _logger, this, SuccessMessages.TransactionsRetrievedByDateRange);

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] FinancialTransactionDTO transactionDTO)
        => await ControllerHelper.ExecuteAsync(async () =>
        {
            if (!ModelState.IsValid)
                throw new Exception("Invalid model.");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                throw new Exception(ErrorMessages.UserNotFound);

            transactionDTO.UserId = userId;

            await _transactionService.CreateAsync(transactionDTO);
        }, _logger, this, SuccessMessages.TransactionCreated);

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] FinancialTransactionDTO transactionDTO)
        => await ControllerHelper.ExecuteAsync(async () =>
        {
            if (id != transactionDTO.Id)
                throw new Exception(ErrorMessages.TransactionIdMismatch);

            if (!ModelState.IsValid)
                throw new Exception("Invalid model.");

            await _transactionService.UpdateAsync(transactionDTO);
        }, _logger, this, SuccessMessages.TransactionUpdated);

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
        => await ControllerHelper.ExecuteAsync(async () =>
        {
            await _transactionService.DeleteAsync(id);
        }, _logger, this, SuccessMessages.TransactionDeleted);
}
