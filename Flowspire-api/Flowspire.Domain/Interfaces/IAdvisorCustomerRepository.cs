using Flowspire.Domain.Entities;

namespace Flowspire.Domain.Interfaces;
public interface IAdvisorCustomerRepository
{
    Task<AdvisorCustomer> AddAsync(AdvisorCustomer advisorCustomer);
    Task<List<AdvisorCustomer>> GetCustomersByAdvisorIdAsync(string advisorId);
    Task<List<AdvisorCustomer>> GetAdvisorsByCustomerIdAsync(string customerId);
}