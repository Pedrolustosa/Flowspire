using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Flowspire.Application.Common;
using Flowspire.Application.DTOs;
using Flowspire.Application.Interfaces;
using Flowspire.Domain.Entities;
using Flowspire.Domain.Interfaces;
using Flowspire.Domain.ValueObjects;             // ← importe aqui
using Microsoft.Extensions.Logging;

namespace Flowspire.Application.Services
{
    public class BudgetService : IBudgetService
    {
        private readonly IBudgetRepository _budgetRepository;
        private readonly ILogger<BudgetService> _logger;

        public BudgetService(
            IBudgetRepository budgetRepository,
            ILogger<BudgetService> logger)
        {
            _budgetRepository = budgetRepository
                ?? throw new ArgumentNullException(nameof(budgetRepository));
            _logger = logger
                ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<BudgetDTO> GetByIdAsync(int id, CancellationToken cancellationToken = default)
            => await ServiceHelper.ExecuteAsync(async () =>
            {
                if (id <= 0) throw new ArgumentException("ID inválido.", nameof(id));
                _logger.LogInformation("Buscando orçamento {BudgetId}", id);

                var budget = await _budgetRepository.GetByIdAsync(id);
                if (budget == null) throw new KeyNotFoundException($"Orçamento {id} não encontrado.");

                return MapToDTO(budget);
            }, _logger, nameof(GetByIdAsync));

        public async Task<IEnumerable<BudgetDTO>> GetAllAsync(CancellationToken cancellationToken = default)
            => await ServiceHelper.ExecuteAsync(async () =>
            {
                _logger.LogInformation("Buscando todos os orçamentos");
                var budgets = await _budgetRepository.GetAllAsync();
                return budgets.Select(MapToDTO).ToList();
            }, _logger, nameof(GetAllAsync));

        public async Task<IEnumerable<BudgetDTO>> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default)
            => await ServiceHelper.ExecuteAsync(async () =>
            {
                if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentException("UserId requerido.", nameof(userId));
                _logger.LogInformation("Buscando orçamentos do usuário {UserId}", userId);
                var budgets = await _budgetRepository.GetByUserIdAsync(userId);
                return budgets.Select(MapToDTO).ToList();
            }, _logger, nameof(GetByUserIdAsync));

        public async Task CreateAsync(BudgetDTO budgetDTO, CancellationToken cancellationToken = default)
            => await ServiceHelper.ExecuteAsync(async () =>
            {
                if (budgetDTO == null) throw new ArgumentNullException(nameof(budgetDTO));
                _logger.LogInformation("Criando orçamento para usuário {UserId}", budgetDTO.UserId);

                var money = Money.Create(budgetDTO.Amount);     // ← converte decimal em ValueObject
                var budget = Budget.Create(
                    money,
                    budgetDTO.StartDate,
                    budgetDTO.EndDate,
                    budgetDTO.CategoryId,
                    budgetDTO.UserId);

                await _budgetRepository.AddAsync(budget);
                budgetDTO.Id = budget.Id;
            }, _logger, nameof(CreateAsync));

        public async Task UpdateAsync(BudgetDTO budgetDTO, CancellationToken cancellationToken = default)
            => await ServiceHelper.ExecuteAsync(async () =>
            {
                if (budgetDTO == null) throw new ArgumentNullException(nameof(budgetDTO));
                _logger.LogInformation("Atualizando orçamento {BudgetId}", budgetDTO.Id);

                var budget = await _budgetRepository.GetByIdAsync(budgetDTO.Id)
                             ?? throw new KeyNotFoundException($"Orçamento {budgetDTO.Id} não encontrado.");

                var money = Money.Create(budgetDTO.Amount);
                budget.Update(
                    money,
                    budgetDTO.StartDate,
                    budgetDTO.EndDate,
                    budgetDTO.CategoryId);

                await _budgetRepository.UpdateAsync(budget);
            }, _logger, nameof(UpdateAsync));

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
            => await ServiceHelper.ExecuteAsync(async () =>
            {
                if (id <= 0) throw new ArgumentException("ID inválido.", nameof(id));
                _logger.LogInformation("Deletando orçamento {BudgetId}", id);

                var budget = await _budgetRepository.GetByIdAsync(id)
                             ?? throw new KeyNotFoundException($"Orçamento {id} não encontrado.");

                await _budgetRepository.DeleteAsync(budget);
            }, _logger, nameof(DeleteAsync));

        public async Task<IEnumerable<BudgetDTO>> GetActiveBudgetsAsync(string userId, DateTime date, CancellationToken cancellationToken = default)
            => await ServiceHelper.ExecuteAsync(async () =>
            {
                if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentException("UserId requerido.", nameof(userId));
                _logger.LogInformation("Buscando orçamentos ativos para usuário {UserId} na data {Date}", userId, date);

                var budgets = await _budgetRepository.GetActiveBudgetsAsync(userId, date);
                return budgets.Select(MapToDTO).ToList();
            }, _logger, nameof(GetActiveBudgetsAsync));

        public async Task<BudgetDTO> GetBudgetByCategoryIdAsync(string userId, int categoryId, CancellationToken cancellationToken = default)
            => await ServiceHelper.ExecuteAsync(async () =>
            {
                if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentException("UserId requerido.", nameof(userId));
                if (categoryId <= 0) throw new ArgumentException("CategoryId inválido.", nameof(categoryId));
                _logger.LogInformation("Buscando orçamento de categoria {CategoryId} para usuário {UserId}", categoryId, userId);

                var budget = await _budgetRepository.GetBudgetByCategoryIdAsync(userId, categoryId)
                             ?? throw new KeyNotFoundException($"Orçamento não encontrado para categoria {categoryId}.");

                return MapToDTO(budget);
            }, _logger, nameof(GetBudgetByCategoryIdAsync));

        private static BudgetDTO MapToDTO(Budget b)
        {
            return new BudgetDTO
            {
                Id           = b.Id,
                Amount       = b.Amount.Value,
                StartDate    = b.StartDate,
                EndDate      = b.EndDate,
                CategoryId   = b.CategoryId,
                UserId       = b.UserId,
                CategoryName = b.Category?.Name ?? string.Empty
            };
        }
    }
}
