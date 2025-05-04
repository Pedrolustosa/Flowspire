namespace Flowspire.Application.Interfaces;

public interface IAdvisorCustomerRepository
{
    Task<AdvisorCustomer> AddAsync(AdvisorCustomer advisorCustomer);
    Task<AdvisorCustomer?> GetByAdvisorAndCustomerAsync(string advisorId, string customerId);
    Task DeleteAsync(AdvisorCustomer advisorCustomer);
    Task<List<AdvisorCustomer>> GetCustomersByAdvisorIdAsync(string advisorId);
    Task<List<AdvisorCustomer>> GetAdvisorsByCustomerIdAsync(string customerId);
}