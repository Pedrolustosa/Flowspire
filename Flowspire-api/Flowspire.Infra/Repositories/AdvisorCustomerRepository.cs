using Microsoft.EntityFrameworkCore;
using Flowspire.Domain.Interfaces;
using Flowspire.Infra.Data;
using Flowspire.Infra.Common;
using Microsoft.Extensions.Logging;
using Flowspire.Application.Interfaces;

namespace Flowspire.Infra.Repositories;

public class AdvisorCustomerRepository(ApplicationDbContext context, ILogger<AdvisorCustomerRepository> logger) : IAdvisorCustomerRepository
{
    private readonly ApplicationDbContext _context = context;
    private readonly ILogger<AdvisorCustomerRepository> _logger = logger;

    public async Task<AdvisorCustomer> AddAsync(AdvisorCustomer advisorCustomer)
    => await RepositoryHelper.ExecuteAsync(async () =>
    {
        await _context.AdvisorCustomers.AddAsync(advisorCustomer);
        await _context.SaveChangesAsync();
        return advisorCustomer;
    }, _logger, nameof(AddAsync));

    public async Task<AdvisorCustomer?> GetByAdvisorAndCustomerAsync(string advisorId, string customerId)
        => await RepositoryHelper.ExecuteAsync(
            () => _context.AdvisorCustomers
                          .FirstOrDefaultAsync(ac => ac.AdvisorId == advisorId
                                                  && ac.CustomerId == customerId),
            _logger,
            nameof(GetByAdvisorAndCustomerAsync));

    public async Task DeleteAsync(AdvisorCustomer advisorCustomer)
        => await RepositoryHelper.ExecuteAsync(async () =>
        {
            _context.AdvisorCustomers.Remove(advisorCustomer);
            await _context.SaveChangesAsync();
        }, _logger, nameof(DeleteAsync));

    public async Task<List<AdvisorCustomer>> GetCustomersByAdvisorIdAsync(string advisorId)
        => await RepositoryHelper.ExecuteAsync(
            () => _context.AdvisorCustomers
                          .Where(ac => ac.AdvisorId == advisorId)
                          .ToListAsync(),
            _logger,
            nameof(GetCustomersByAdvisorIdAsync));

    public async Task<List<AdvisorCustomer>> GetAdvisorsByCustomerIdAsync(string customerId)
        => await RepositoryHelper.ExecuteAsync(
            () => _context.AdvisorCustomers
                          .Where(ac => ac.CustomerId == customerId)
                          .ToListAsync(),
            _logger,
            nameof(GetAdvisorsByCustomerIdAsync));
}
