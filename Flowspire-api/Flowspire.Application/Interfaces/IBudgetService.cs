using Flowspire.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flowspire.Application.Interfaces
{
    public interface IBudgetService
    {
        Task<BudgetDTO> AddBudgetAsync(BudgetDTO budgetDto);
        Task<List<BudgetDTO>> GetBudgetsByUserIdAsync(string userId);
        Task CheckBudgetAndNotifyAsync(string userId, int categoryId, decimal transactionAmount);
    }
}
