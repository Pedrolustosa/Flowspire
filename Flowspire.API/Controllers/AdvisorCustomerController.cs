using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Flowspire.Application.Interfaces;
using Flowspire.Application.DTOs;

namespace Flowspire.API.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AdvisorCustomerController : ControllerBase
{
    private readonly IAdvisorCustomerService _advisorCustomerService;

    public AdvisorCustomerController(IAdvisorCustomerService advisorCustomerService)
    {
        _advisorCustomerService = advisorCustomerService;
    }

    [HttpPost("assign")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> AssignAdvisor([FromBody] AssignAdvisorRequest request)
    {
        var result = await _advisorCustomerService.AssignAdvisorAsync(request.AdvisorId, request.CustomerId);
        return Ok(result);
    }

    [HttpGet("advisor/{advisorId}/customers")]
    [Authorize(Roles = "FinancialAdvisor, Administrator")]
    public async Task<IActionResult> GetCustomers(string advisorId)
    {
        var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (currentUserId != advisorId && !User.IsInRole("Administrator"))
            return Forbid();

        var customers = await _advisorCustomerService.GetCustomersByAdvisorIdAsync(advisorId);
        return Ok(customers);
    }

    [HttpGet("customer/{customerId}/advisors")]
    [Authorize(Roles = "Customer, Administrator")]
    public async Task<IActionResult> GetAdvisors(string customerId)
    {
        var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (currentUserId != customerId && !User.IsInRole("Administrator"))
            return Forbid();

        var advisors = await _advisorCustomerService.GetAdvisorsByCustomerIdAsync(customerId);
        return Ok(advisors);
    }
}

public class AssignAdvisorRequest
{
    public string AdvisorId { get; set; }
    public string CustomerId { get; set; }
}