using Flowspire.Domain.Entities;

public interface IBudgetRepository
{
    Task<Budget> AddAsync(Budget budget);
    Task<List<Budget>> GetByUserIdAsync(string userId);
}