using Flowspire.Application.DTOs;

namespace Flowspire.Application.Interfaces;

public interface IAdvisorCustomerService
{
    Task<AdvisorCustomerDTO> AssignAdvisorAsync(
        string advisorId,
        string customerId,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<AdvisorCustomerDTO>> GetCustomersByAdvisorIdAsync(
        string advisorId,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<AdvisorCustomerDTO>> GetAdvisorsByCustomerIdAsync(
        string customerId,
        CancellationToken cancellationToken = default);

    Task UnassignAdvisorAsync(
        string advisorId,
        string customerId,
        CancellationToken cancellationToken = default);
}