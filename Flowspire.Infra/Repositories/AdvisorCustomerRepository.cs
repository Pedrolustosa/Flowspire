using Microsoft.EntityFrameworkCore;
using Flowspire.Domain.Entities;
using Flowspire.Infra.Data;
using Flowspire.Domain.Interfaces;

namespace Flowspire.Infra.Repositories;
public class AdvisorCustomerRepository : IAdvisorCustomerRepository
{
    private readonly ApplicationDbContext _context;

    public AdvisorCustomerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AdvisorCustomer> AddAsync(AdvisorCustomer advisorCustomer)
    {
        _context.AdvisorCustomers.Add(advisorCustomer);
        await _context.SaveChangesAsync();
        return advisorCustomer;
    }

    public async Task<List<AdvisorCustomer>> GetCustomersByAdvisorIdAsync(string advisorId)
    {
        return await _context.AdvisorCustomers
            .Where(ac => ac.AdvisorId == advisorId)
            .Include(ac => ac.Customer)
            .ToListAsync();
    }

    public async Task<List<AdvisorCustomer>> GetAdvisorsByCustomerIdAsync(string customerId)
    {
        return await _context.AdvisorCustomers
            .Where(ac => ac.CustomerId == customerId)
            .Include(ac => ac.Advisor)
            .ToListAsync();
    }
}