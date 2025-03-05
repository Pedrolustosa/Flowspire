using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Flowspire.Application.Interfaces;
using Flowspire.Application.DTOs;
using Flowspire.API.Models;

namespace Flowspire.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AdvisorCustomerController(IAdvisorCustomerService advisorCustomerService) : ControllerBase
{
    private readonly IAdvisorCustomerService _advisorCustomerService = advisorCustomerService;

    [HttpPost("assign")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> AssignAdvisor([FromBody] AssignAdvisorRequest request)
    {
        try
        {
            var result = await _advisorCustomerService.AssignAdvisorAsync(request.AdvisorId, request.CustomerId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpGet("advisor/{advisorId}/customers")]
    [Authorize(Roles = "FinancialAdvisor, Administrator")]
    public async Task<IActionResult> GetCustomers(string advisorId)
    {
        try
        {
            var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (currentUserId != advisorId && !User.IsInRole("Administrator"))
                return Forbid();

            var customers = await _advisorCustomerService.GetCustomersByAdvisorIdAsync(advisorId);
            return Ok(customers);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpGet("customer/{customerId}/advisors")]
    [Authorize(Roles = "Customer, Administrator")]
    public async Task<IActionResult> GetAdvisors(string customerId)
    {
        try
        {
            var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (currentUserId != customerId && !User.IsInRole("Administrator"))
                return Forbid();

            var advisors = await _advisorCustomerService.GetAdvisorsByCustomerIdAsync(customerId);
            return Ok(advisors);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}