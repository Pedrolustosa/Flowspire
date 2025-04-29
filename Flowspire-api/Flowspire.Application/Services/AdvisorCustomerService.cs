using Flowspire.Application.Interfaces;
using Flowspire.Domain.Entities;
using Flowspire.Application.DTOs;
using Flowspire.Application.Common;
using Microsoft.EntityFrameworkCore;
using Flowspire.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Flowspire.Domain.Enums;

namespace Flowspire.Application.Services;

public class AdvisorCustomerService(IAdvisorCustomerRepository advisorCustomerRepository,
                                    IUserService userService,
                                    ILogger<AdvisorCustomerService> logger) : IAdvisorCustomerService
{
    private readonly IAdvisorCustomerRepository _advisorCustomerRepository = advisorCustomerRepository;
    private readonly IUserService _userService = userService;
    private readonly ILogger<AdvisorCustomerService> _logger = logger;

    public async Task<AdvisorCustomerDTO> AssignAdvisorAsync(string advisorId, string customerId)
    {
        return await ServiceHelper.ExecuteAsync(async () =>
        {
            var advisor = await _userService.GetCurrentUserAsync(advisorId);
            if (!advisor.Roles.Contains(UserRole.FinancialAdvisor))
                throw new InvalidOperationException("The specified user is not a Financial Advisor.");

            var customer = await _userService.GetCurrentUserAsync(customerId);
            if (!customer.Roles.Contains(UserRole.Customer))
                throw new InvalidOperationException("The specified user is not a Customer.");

            var advisorCustomer = AdvisorCustomer.Create(advisorId, customerId);
            var added = await _advisorCustomerRepository.AddAsync(advisorCustomer);

            return new AdvisorCustomerDTO
            {
                AdvisorId = added.AdvisorId,
                CustomerId = added.CustomerId,
                AssignedAt = added.AssignedAt
            };
        }, _logger, nameof(AssignAdvisorAsync));
    }

    public async Task<List<AdvisorCustomerDTO>> GetCustomersByAdvisorIdAsync(string advisorId)
    {
        return await ServiceHelper.ExecuteAsync(async () =>
        {
            var advisorCustomers = await _advisorCustomerRepository.GetCustomersByAdvisorIdAsync(advisorId);
            return advisorCustomers.Select(ac => new AdvisorCustomerDTO
            {
                AdvisorId = ac.AdvisorId,
                CustomerId = ac.CustomerId,
                AssignedAt = ac.AssignedAt
            }).ToList();
        }, _logger, nameof(GetCustomersByAdvisorIdAsync));
    }

    public async Task<List<AdvisorCustomerDTO>> GetAdvisorsByCustomerIdAsync(string customerId)
    {
        return await ServiceHelper.ExecuteAsync(async () =>
        {
            var advisorCustomers = await _advisorCustomerRepository.GetAdvisorsByCustomerIdAsync(customerId);
            return advisorCustomers.Select(ac => new AdvisorCustomerDTO
            {
                AdvisorId = ac.AdvisorId,
                CustomerId = ac.CustomerId,
                AssignedAt = ac.AssignedAt
            }).ToList();
        }, _logger, nameof(GetAdvisorsByCustomerIdAsync));
    }
}
