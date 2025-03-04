using Flowspire.Application.Interfaces;
using Flowspire.Domain.Entities;
using Flowspire.Application.DTOs;
using Flowspire.Domain.Interfaces;

namespace Flowspire.Application.Services;
public class AdvisorCustomerService : IAdvisorCustomerService
{
    private readonly IAdvisorCustomerRepository _advisorCustomerRepository;
    private readonly IUserService _userService;

    public AdvisorCustomerService(IAdvisorCustomerRepository advisorCustomerRepository, IUserService userService)
    {
        _advisorCustomerRepository = advisorCustomerRepository;
        _userService = userService;
    }

    public async Task<AdvisorCustomerDTO> AssignAdvisorAsync(string advisorId, string customerId)
    {
        var advisor = await _userService.GetCurrentUserAsync(advisorId);
        if (advisor.Role != Flowspire.Domain.Enums.UserRole.FinancialAdvisor)
            throw new Exception("O usuário especificado não é um FinancialAdvisor.");

        var customer = await _userService.GetCurrentUserAsync(customerId);
        if (customer.Role != Flowspire.Domain.Enums.UserRole.Customer)
            throw new Exception("O usuário especificado não é um Customer.");

        var advisorCustomer = AdvisorCustomer.Create(advisorId, customerId);
        var added = await _advisorCustomerRepository.AddAsync(advisorCustomer);
        return new AdvisorCustomerDTO
        {
            AdvisorId = added.AdvisorId,
            CustomerId = added.CustomerId,
            AssignedAt = added.AssignedAt
        };
    }

    public async Task<List<AdvisorCustomerDTO>> GetCustomersByAdvisorIdAsync(string advisorId)
    {
        var advisorCustomers = await _advisorCustomerRepository.GetCustomersByAdvisorIdAsync(advisorId);
        return advisorCustomers.Select(ac => new AdvisorCustomerDTO
        {
            AdvisorId = ac.AdvisorId,
            CustomerId = ac.CustomerId,
            AssignedAt = ac.AssignedAt
        }).ToList();
    }

    public async Task<List<AdvisorCustomerDTO>> GetAdvisorsByCustomerIdAsync(string customerId)
    {
        var advisorCustomers = await _advisorCustomerRepository.GetAdvisorsByCustomerIdAsync(customerId);
        return advisorCustomers.Select(ac => new AdvisorCustomerDTO
        {
            AdvisorId = ac.AdvisorId,
            CustomerId = ac.CustomerId,
            AssignedAt = ac.AssignedAt
        }).ToList();
    }
}