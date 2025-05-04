// Application/Interfaces/IBudgetService.cs
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Flowspire.Application.DTOs;

namespace Flowspire.Application.Interfaces;

public interface IBudgetService
{
    Task<BudgetDTO> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<BudgetDTO>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<IEnumerable<BudgetDTO>> GetByUserIdAsync(
        string userId,
        CancellationToken cancellationToken = default);

    Task CreateAsync(
        BudgetDTO budgetDTO,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        BudgetDTO budgetDTO,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<BudgetDTO>> GetActiveBudgetsAsync(
        string userId,
        DateTime date,
        CancellationToken cancellationToken = default);

    Task<BudgetDTO> GetBudgetByCategoryIdAsync(
        string userId,
        int categoryId,
        CancellationToken cancellationToken = default);
}
