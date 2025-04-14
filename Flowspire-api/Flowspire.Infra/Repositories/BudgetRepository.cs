using Microsoft.EntityFrameworkCore;
using Flowspire.Infra.Data;
using Flowspire.Domain.Interfaces;

namespace Flowspire.Infra.Repositories;

public class BudgetRepository(ApplicationDbContext context) : IBudgetRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Budget> GetByIdAsync(int id)
    {
        return await _context.Budgets.FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<IEnumerable<Budget>> GetAllAsync()
    {
        return await _context.Budgets.ToListAsync();
    }

    public async Task<IEnumerable<Budget>> GetByUserIdAsync(string userId)
    {
        return await _context.Budgets.Where(b => b.UserId == userId).ToListAsync();
    }

    public async Task AddAsync(Budget budget)
    {
        await _context.Budgets.AddAsync(budget);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Budget budget)
    {
        _context.Budgets.Update(budget);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Budget budget)
    {
        _context.Budgets.Remove(budget);
        await _context.SaveChangesAsync();
    }
    public async Task<IEnumerable<Budget>> GetActiveBudgetsAsync(string userId, DateTime date)
    {
        return await _context.Budgets
            .Where(b => b.UserId == userId && b.StartDate <= date && b.EndDate >= date)
            .ToListAsync();
    }
    public async Task<Budget> GetBudgetByCategoryIdAsync(string userId, int categoryId)
    {
        return await _context.Budgets
            .FirstOrDefaultAsync(b => b.UserId == userId && b.CategoryId == categoryId);
    }
}
