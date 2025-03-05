using Microsoft.EntityFrameworkCore;
using Flowspire.Domain.Entities;
using Flowspire.Infra.Data;
using Flowspire.Domain.Interfaces;
using Microsoft.Data.Sqlite;

namespace Flowspire.Infra.Repositories;
public class AdvisorCustomerRepository(ApplicationDbContext context) : IAdvisorCustomerRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<AdvisorCustomer> AddAsync(AdvisorCustomer advisorCustomer)
    {
        try
        {
            _context.AdvisorCustomers.Add(advisorCustomer);
            await _context.SaveChangesAsync();
            return advisorCustomer;
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Erro ao adicionar a associação advisor-customer ao banco de dados.", ex);
        }
        catch (SqliteException ex)
        {
            throw new Exception("Erro de conexão ou operação no SQLite ao adicionar associação.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro inesperado ao adicionar associação advisor-customer.", ex);
        }
    }

    public async Task<List<AdvisorCustomer>> GetCustomersByAdvisorIdAsync(string advisorId)
    {
        try
        {
            return await _context.AdvisorCustomers
                .Where(ac => ac.AdvisorId == advisorId)
                .Include(ac => ac.Customer)
                .ToListAsync();
        }
        catch (SqliteException ex)
        {
            throw new Exception("Erro de conexão ou operação no SQLite ao recuperar clientes do advisor.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro inesperado ao recuperar clientes do advisor.", ex);
        }
    }

    public async Task<List<AdvisorCustomer>> GetAdvisorsByCustomerIdAsync(string customerId)
    {
        try
        {
            return await _context.AdvisorCustomers
                .Where(ac => ac.CustomerId == customerId)
                .Include(ac => ac.Advisor)
                .ToListAsync();
        }
        catch (SqliteException ex)
        {
            throw new Exception("Erro de conexão ou operação no SQLite ao recuperar advisors do cliente.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro inesperado ao recuperar advisors do cliente.", ex);
        }
    }
}