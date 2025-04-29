using Microsoft.EntityFrameworkCore;
using Flowspire.Domain.Entities;
using Flowspire.Domain.Interfaces;
using Flowspire.Infra.Data;
using Flowspire.Infra.Common;
using Microsoft.Extensions.Logging;

namespace Flowspire.Infra.Repositories;

public class AdvisorCustomerRepository(ApplicationDbContext context, ILogger<AdvisorCustomerRepository> logger) : IAdvisorCustomerRepository
{
    private readonly ApplicationDbContext _context = context;
    private readonly ILogger<AdvisorCustomerRepository> _logger = logger;

    public async Task<AdvisorCustomer> AddAsync(AdvisorCustomer advisorCustomer)
    {
        return await RepositoryHelper.ExecuteAsync(async () =>
        {
            _context.AdvisorCustomers.Add(advisorCustomer);
            await _context.SaveChangesAsync();
            return advisorCustomer;
        }, _logger, nameof(AddAsync));
    }

    public async Task<List<AdvisorCustomer>> GetCustomersByAdvisorIdAsync(string advisorId)
    {
        return await RepositoryHelper.ExecuteAsync(async () =>
        {
            return await _context.AdvisorCustomers
                .Where(ac => ac.AdvisorId == advisorId)
                .Include(ac => ac.Customer)
                .ToListAsync();
        }, _logger, nameof(GetCustomersByAdvisorIdAsync));
    }

    public async Task<List<AdvisorCustomer>> GetAdvisorsByCustomerIdAsync(string customerId)
    {
        return await RepositoryHelper.ExecuteAsync(async () =>
        {
            return await _context.AdvisorCustomers
                .Where(ac => ac.CustomerId == customerId)
                .Include(ac => ac.Advisor)
                .ToListAsync();
        }, _logger, nameof(GetAdvisorsByCustomerIdAsync));
    }
}