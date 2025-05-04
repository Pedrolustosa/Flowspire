using Flowspire.Application.Common;
using Flowspire.Application.DTOs;
using Flowspire.Application.Interfaces;
using Flowspire.Domain.Entities;
using Flowspire.Domain.Enums;
using Flowspire.Domain.Interfaces;
using Flowspire.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Flowspire.Application.Services
{
    public class FinancialTransactionService : IFinancialTransactionService
    {
        private readonly IFinancialTransactionRepository _repo;
        private readonly ILogger<FinancialTransactionService> _logger;

        public FinancialTransactionService(
            IFinancialTransactionRepository repo,
            ILogger<FinancialTransactionService> logger)
        {
            _repo   = repo    ?? throw new ArgumentNullException(nameof(repo));
            _logger = logger  ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<FinancialTransactionDTO> GetByIdAsync(int id)
            => await ServiceHelper.ExecuteAsync(async () =>
            {
                var e = await _repo.GetByIdAsync(id);
                return e is null ? null : MapToDto(e);
            }, _logger, nameof(GetByIdAsync));

        public async Task<IEnumerable<FinancialTransactionDTO>> GetAllAsync()
            => await ServiceHelper.ExecuteAsync(async () =>
                (await _repo.GetAllAsync()).Select(MapToDto).ToList(),
                _logger, nameof(GetAllAsync));

        public async Task<IEnumerable<FinancialTransactionDTO>> GetByUserIdAsync(string userId)
            => await ServiceHelper.ExecuteAsync(async () =>
                (await _repo.GetByUserIdAsync(userId)).Select(MapToDto).ToList(),
                _logger, nameof(GetByUserIdAsync));

        public async Task<IEnumerable<FinancialTransactionDTO>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
            => await ServiceHelper.ExecuteAsync(async () =>
                (await _repo.GetTransactionsByDateRangeAsync(startDate, endDate))
                    .Select(MapToDto)
                    .ToList(),
                _logger, nameof(GetByDateRangeAsync));

        public async Task CreateAsync(FinancialTransactionDTO dto)
            => await ServiceHelper.ExecuteAsync(async () =>
            {
                if (!Enum.TryParse<TransactionType>(dto.TransactionType, true, out var type))
                    throw new ArgumentException("Tipo inválido.", nameof(dto.TransactionType));

                var entity = FinancialTransaction.Create(
                    dto.Description,
                    new Money(dto.Amount),
                    dto.Date,
                    type,
                    dto.CategoryId,
                    dto.UserId,
                    dto.Fee.HasValue ? new Money(dto.Fee.Value) : null,
                    dto.Discount.HasValue ? new Money(dto.Discount.Value) : null,
                    dto.Notes,
                    dto.PaymentMethod,
                    dto.IsRecurring,
                    dto.NextOccurrence
                );

                await _repo.AddAsync(entity);
            }, _logger, nameof(CreateAsync));

        public async Task UpdateAsync(FinancialTransactionDTO dto)
            => await ServiceHelper.ExecuteAsync(async () =>
            {
                var e = await _repo.GetByIdAsync(dto.Id)
                      ?? throw new KeyNotFoundException("Transação não encontrada.");

                if (!Enum.TryParse<TransactionType>(dto.TransactionType, true, out var type))
                    throw new ArgumentException("Tipo inválido.", nameof(dto.TransactionType));

                e.Update(
                    dto.Description,
                    new Money(dto.Amount),
                    type,
                    dto.CategoryId,
                    dto.Fee.HasValue ? new Money(dto.Fee.Value) : null,
                    dto.Discount.HasValue ? new Money(dto.Discount.Value) : null,
                    dto.Notes,
                    dto.PaymentMethod
                );

                await _repo.UpdateAsync(e);
            }, _logger, nameof(UpdateAsync));

        public async Task DeleteAsync(int id)
            => await ServiceHelper.ExecuteAsync(async () =>
            {
                var e = await _repo.GetByIdAsync(id)
                      ?? throw new KeyNotFoundException("Transação não encontrada.");
                await _repo.DeleteAsync(e);
            }, _logger, nameof(DeleteAsync));

        private FinancialTransactionDTO MapToDto(FinancialTransaction e) => new()
        {
            Id               = e.Id,
            Description      = e.Description,
            Amount           = e.Amount,
            Fee              = e.Fee,
            Discount         = e.Discount,
            Date             = e.Date,
            TransactionType  = e.Type.ToString(),
            CategoryId       = e.CategoryId,
            CategoryName     = e.Category?.Name ?? string.Empty,
            UserId           = e.UserId,
            CreatedAt        = e.CreatedAt,
            UpdatedAt        = e.UpdatedAt,
            Notes            = e.Notes,
            PaymentMethod    = e.PaymentMethod,
            IsRecurring      = e.IsRecurring,
            NextOccurrence   = e.NextOccurrence
        };
    }
}
