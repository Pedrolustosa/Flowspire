using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Flowspire.Application.Interfaces;
using Flowspire.API.Common;
using Flowspire.Application.Common;
using System.Security.Claims;
using Flowspire.API.Models.Users;

namespace Flowspire.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AdvisorCustomerController(IAdvisorCustomerService advisorCustomerService, ILogger<AdvisorCustomerController> logger) : ControllerBase
{
    private readonly IAdvisorCustomerService _advisorCustomerService = advisorCustomerService;
    private readonly ILogger<AdvisorCustomerController> _logger = logger;

    [HttpPost("assign")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> AssignAdvisor([FromBody] UserAdvisorAssignmentRequest request)
        => await ControllerHelper.ExecuteAsync(async () =>
        {
            var result = await _advisorCustomerService.AssignAdvisorAsync(request.AdvisorId, request.CustomerId);
            return result;
        }, _logger, this, SuccessMessages.AdvisorAssigned);

    [HttpGet("advisor/{advisorId}/customers")]
    [Authorize(Roles = "FinancialAdvisor,Administrator")]
    public async Task<IActionResult> GetCustomers(string advisorId)
        => await ControllerHelper.ExecuteAsync(async () =>
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (currentUserId != advisorId && !User.IsInRole("Administrator"))
                throw new UnauthorizedAccessException(ErrorMessages.ForbiddenAccess);

            var customers = await _advisorCustomerService.GetCustomersByAdvisorIdAsync(advisorId);
            return customers;
        }, _logger, this, SuccessMessages.CustomersRetrievedByAdvisor);

    [HttpGet("customer/{customerId}/advisors")]
    [Authorize(Roles = "Customer,Administrator")]
    public async Task<IActionResult> GetAdvisors(string customerId)
        => await ControllerHelper.ExecuteAsync(async () =>
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (currentUserId != customerId && !User.IsInRole("Administrator"))
                throw new UnauthorizedAccessException(ErrorMessages.ForbiddenAccess);

            var advisors = await _advisorCustomerService.GetAdvisorsByCustomerIdAsync(customerId);
            return advisors;
        }, _logger, this, SuccessMessages.AdvisorsRetrievedByCustomer);
}
