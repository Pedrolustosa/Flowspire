using Microsoft.EntityFrameworkCore;
using Flowspire.Domain.Entities;
using Flowspire.Infra.Data;
using Microsoft.Data.Sqlite;

namespace Flowspire.Infra.Repositories;
public class BudgetRepository(ApplicationDbContext context) : IBudgetRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Budget> AddAsync(Budget budget)
    {
        try
        {
            _context.Budgets.Add(budget);
            await _context.SaveChangesAsync();
            return budget;
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Erro ao adicionar o orçamento ao banco de dados.", ex);
        }
        catch (SqliteException ex)
        {
            throw new Exception("Erro de conexão ou operação no SQLite ao adicionar orçamento.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro inesperado ao adicionar orçamento.", ex);
        }
    }

    public async Task<List<Budget>> GetByUserIdAsync(string userId)
    {
        try
        {
            return await _context.Budgets
                .Where(b => b.UserId == userId)
                .Include(b => b.Category)
                .ToListAsync();
        }
        catch (SqliteException ex)
        {
            throw new Exception("Erro de conexão ou operação no SQLite ao recuperar orçamentos.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro inesperado ao recuperar orçamentos.", ex);
        }
    }
}