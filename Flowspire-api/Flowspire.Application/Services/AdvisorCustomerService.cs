using Flowspire.Application.Interfaces;
using Flowspire.Domain.Entities;
using Flowspire.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using Flowspire.Domain.Interfaces;

namespace Flowspire.Application.Services;

public class AdvisorCustomerService(IAdvisorCustomerRepository advisorCustomerRepository, 
                                    IUserService userService) : IAdvisorCustomerService
{
    private readonly IAdvisorCustomerRepository _advisorCustomerRepository = advisorCustomerRepository;
    private readonly IUserService _userService = userService;

    public async Task<AdvisorCustomerDTO> AssignAdvisorAsync(string advisorId, string customerId)
    {
        try
        {
            var advisor = await _userService.GetCurrentUserAsync(advisorId);
            if (!advisor.Roles.Contains(Flowspire.Domain.Enums.UserRole.FinancialAdvisor))
                throw new Exception("O usuário especificado não é um FinancialAdvisor.");

            var customer = await _userService.GetCurrentUserAsync(customerId);
            if (!customer.Roles.Contains(Flowspire.Domain.Enums.UserRole.Customer))
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
        catch (DbUpdateException ex)
        {
            throw new Exception("Erro ao associar advisor e customer no banco de dados.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro inesperado ao associar advisor e customer.", ex);
        }
    }
    public async Task<List<AdvisorCustomerDTO>> GetCustomersByAdvisorIdAsync(string advisorId)
    {
        try
        {
            var advisorCustomers = await _advisorCustomerRepository.GetCustomersByAdvisorIdAsync(advisorId);
            return advisorCustomers.Select(ac => new AdvisorCustomerDTO
            {
                AdvisorId = ac.AdvisorId,
                CustomerId = ac.CustomerId,
                AssignedAt = ac.AssignedAt
            }).ToList();
        }
        catch (Exception ex)
        {
            throw new Exception("Erro inesperado ao recuperar clientes do advisor.", ex);
        }
    }

    public async Task<List<AdvisorCustomerDTO>> GetAdvisorsByCustomerIdAsync(string customerId)
    {
        try
        {
            var advisorCustomers = await _advisorCustomerRepository.GetAdvisorsByCustomerIdAsync(customerId);
            return advisorCustomers.Select(ac => new AdvisorCustomerDTO
            {
                AdvisorId = ac.AdvisorId,
                CustomerId = ac.CustomerId,
                AssignedAt = ac.AssignedAt
            }).ToList();
        }
        catch (Exception ex)
        {
            throw new Exception("Erro inesperado ao recuperar advisors do cliente.", ex);
        }
    }
}