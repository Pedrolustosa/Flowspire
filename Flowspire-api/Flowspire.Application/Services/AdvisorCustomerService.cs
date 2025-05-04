using Flowspire.Application.Common;
using Flowspire.Application.DTOs;
using Flowspire.Application.Interfaces;
using Flowspire.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace Flowspire.Application.Services;

public class AdvisorCustomerService(
    IAdvisorCustomerRepository advisorCustomerRepository,
    IUserService userService,
    ILogger<AdvisorCustomerService> logger) : IAdvisorCustomerService
{
    private readonly IAdvisorCustomerRepository _advisorCustomerRepository = advisorCustomerRepository
            ?? throw new ArgumentNullException(nameof(advisorCustomerRepository));
    private readonly IUserService _userService = userService
            ?? throw new ArgumentNullException(nameof(userService));
    private readonly ILogger<AdvisorCustomerService> _logger = logger
            ?? throw new ArgumentNullException(nameof(logger));

    public async Task<AdvisorCustomerDTO> AssignAdvisorAsync(
        string advisorId,
        string customerId,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(advisorId))
            throw new ArgumentException("Advisor ID must be provided.", nameof(advisorId));
        if (string.IsNullOrWhiteSpace(customerId))
            throw new ArgumentException("Customer ID must be provided.", nameof(customerId));

        return await ServiceHelper.ExecuteAsync(async () =>
        {
            cancellationToken.ThrowIfCancellationRequested();
            _logger.LogInformation("Assigning advisor {AdvisorId} to customer {CustomerId}...", advisorId, customerId);

            // Evita duplicação
            var existing = await _advisorCustomerRepository
                .GetByAdvisorAndCustomerAsync(advisorId, customerId);
            if (existing != null)
                throw new InvalidOperationException("This advisor is already assigned to the customer.");

            // Valida roles
            var advisor = await _userService.GetCurrentUserAsync(advisorId);
            if (!advisor.Roles.Contains(UserRole.FinancialAdvisor))
                throw new InvalidOperationException("Specified user is not a Financial Advisor.");

            var customer = await _userService.GetCurrentUserAsync(customerId);
            if (!customer.Roles.Contains(UserRole.Customer))
                throw new InvalidOperationException("Specified user is not a Customer.");

            var relation = AdvisorCustomer.Create(advisorId, customerId);
            var added = await _advisorCustomerRepository.AddAsync(relation);

            _logger.LogInformation("Advisor {AdvisorId} assigned to customer {CustomerId} successfully.", advisorId, customerId);

            return new AdvisorCustomerDTO
            {
                AdvisorId = added.AdvisorId,
                CustomerId = added.CustomerId,
                AssignedAt = added.AssignedAt
            };
        }, _logger, nameof(AssignAdvisorAsync));
    }

    public async Task<IEnumerable<AdvisorCustomerDTO>> GetCustomersByAdvisorIdAsync(
        string advisorId,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(advisorId))
            throw new ArgumentException("Advisor ID must be provided.", nameof(advisorId));

        return await ServiceHelper.ExecuteAsync(async () =>
        {
            cancellationToken.ThrowIfCancellationRequested();
            _logger.LogInformation("Retrieving customers for advisor {AdvisorId}...", advisorId);

            var items = await _advisorCustomerRepository.GetCustomersByAdvisorIdAsync(advisorId);
            var dtos = items.Select(ac => new AdvisorCustomerDTO
            {
                AdvisorId = ac.AdvisorId,
                CustomerId = ac.CustomerId,
                AssignedAt = ac.AssignedAt
            });

            _logger.LogInformation("Retrieved {Count} customers for advisor {AdvisorId}.", dtos.Count(), advisorId);
            return dtos;
        }, _logger, nameof(GetCustomersByAdvisorIdAsync));
    }

    public async Task<IEnumerable<AdvisorCustomerDTO>> GetAdvisorsByCustomerIdAsync(
        string customerId,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(customerId))
            throw new ArgumentException("Customer ID must be provided.", nameof(customerId));

        return await ServiceHelper.ExecuteAsync(async () =>
        {
            cancellationToken.ThrowIfCancellationRequested();
            _logger.LogInformation("Retrieving advisors for customer {CustomerId}...", customerId);

            var items = await _advisorCustomerRepository.GetAdvisorsByCustomerIdAsync(customerId);
            var dtos = items.Select(ac => new AdvisorCustomerDTO
            {
                AdvisorId = ac.AdvisorId,
                CustomerId = ac.CustomerId,
                AssignedAt = ac.AssignedAt
            });

            _logger.LogInformation("Retrieved {Count} advisors for customer {CustomerId}.", dtos.Count(), customerId);
            return dtos;
        }, _logger, nameof(GetAdvisorsByCustomerIdAsync));
    }

    public async Task UnassignAdvisorAsync(
        string advisorId,
        string customerId,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(advisorId))
            throw new ArgumentException("Advisor ID must be provided.", nameof(advisorId));
        if (string.IsNullOrWhiteSpace(customerId))
            throw new ArgumentException("Customer ID must be provided.", nameof(customerId));

        await ServiceHelper.ExecuteAsync(async () =>
        {
            cancellationToken.ThrowIfCancellationRequested();
            _logger.LogInformation("Unassigning advisor {AdvisorId} from customer {CustomerId}...", advisorId, customerId);

            var existing = await _advisorCustomerRepository.GetByAdvisorAndCustomerAsync(advisorId, customerId);
            if (existing == null)
                throw new KeyNotFoundException($"No assignment found for advisor '{advisorId}' and customer '{customerId}'.");

            await _advisorCustomerRepository.DeleteAsync(existing);

            _logger.LogInformation("Advisor {AdvisorId} unassigned from customer {CustomerId} successfully.", advisorId, customerId);
        }, _logger, nameof(UnassignAdvisorAsync));
    }
}
