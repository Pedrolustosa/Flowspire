using Flowspire.Application.DTOs;

namespace Flowspire.Application.Interfaces;
public interface IAdvisorCustomerService
{
    Task<AdvisorCustomerDTO> AssignAdvisorAsync(string advisorId, string customerId);
    Task<List<AdvisorCustomerDTO>> GetCustomersByAdvisorIdAsync(string advisorId);
    Task<List<AdvisorCustomerDTO>> GetAdvisorsByCustomerIdAsync(string customerId);
}